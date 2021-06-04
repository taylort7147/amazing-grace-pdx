using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MessageManager.Areas.Identity.Authorization;
using MessageManager.Data;
using MessageManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MessageManager.Pages.Messages
{
    [Authorize(Policy = Constants.ReadWritePolicy)]
    public class EditModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;
        private readonly ILogger _logger;

        public EditModel(MessageManager.Data.MessageContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Message Message { get; set; }


        private static bool ReferenceCompare(BibleReferenceRange x, BibleReferenceRange y)
        {
            return
                x.StartBook == y.StartBook &&
                x.StartChapter == y.StartChapter &&
                x.StartVerse == y.StartVerse &&
                x.EndBook == y.EndBook &&
                x.EndChapter == y.EndChapter &&
                x.EndVerse == y.EndVerse;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Message = await _context.Message
                .Include(m => m.BibleReferences)
                .FirstOrDefaultAsync(m => m.Id == id);

            var seriesSelectList = new SelectList(_context.Series, "Id", "Name");
            var selected = seriesSelectList.Where(x => x.Value == Message.SeriesId.ToString()).FirstOrDefault();
            if (selected != null)
            {
                selected.Selected = true;
            }
            ViewData["SeriesSelectList"] = seriesSelectList;

            if (Message == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Message).State = EntityState.Modified;

            try
            {
                // Bible reference handling
                {
                    var newReferences = Message.BibleReferences.Where(x => x.Id == 0);
                    var existingMessageReferences = Message.BibleReferences.Where(x => x.Id != 0);
                    var existingDbReferences = await _context.BibleReferences.Where(x => x.MessageId == Message.Id).ToListAsync();

                    // Remove references that are no longer present
                    foreach (var reference in existingDbReferences)
                    {
                        if (!newReferences.Any(x => ReferenceCompare(x, reference)))
                        {
                            Message.BibleReferences.RemoveAll(x => ReferenceCompare(x, reference));
                            _context.BibleReferences.RemoveRange(_context.BibleReferences.Where(x => x.Id == reference.Id));
                        }
                    }

                    // Add new references
                    var referencesToRemove = new List<BibleReferenceRange>();
                    foreach (var reference in newReferences)
                    {
                        if (existingMessageReferences.Any(x => ReferenceCompare(x, reference)))
                        {
                            referencesToRemove.Add(reference);
                        }
                        if (!existingDbReferences.Any(x => ReferenceCompare(x, reference)))
                        {
                            _context.BibleReferences.Add(reference);
                        }
                    }
                    foreach (var reference in referencesToRemove)
                    {
                        Message.BibleReferences.Remove(reference);
                    }
                }

                await _context.SaveChangesAsync();
                _logger.LogCritical($"User '{User.Identity.Name}' edited object with new values'{Message.ToString()}'.");

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(Message.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.Id == id);
        }
    }
}
