using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MessageManager.Authorization;
using MessageManager.Models;

namespace MessageManager.Pages_Notes
{
    [Authorize(Policy = Constants.ReadWritePolicy)]
    public class EditModel : PageModel
    {
        private readonly MessageContext _context;
        private readonly ILogger _logger;

        public EditModel(MessageContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Notes Notes { get; set; }

        [BindProperty]
        public int? OriginalMessageId { get; set; }

        public SelectList MessageIdList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Notes = await _context.Notes
                    .Include(n => n.Message).FirstOrDefaultAsync(m => m.Id == id);

            if (Notes == null)
            {
                return NotFound();
            }

            OriginalMessageId = Notes.MessageId;

            // Only show messages that don't have a linked notes reference, or are already linked to this
            var selectableMessages = _context.Message.Where(m => m.NotesId == null || m.NotesId == Notes.Id);

            MessageIdList = new SelectList(selectableMessages, "Id", "Description");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var message = await _context.Message.FindAsync(Notes.MessageId);
            if(message == null)
            {
                Console.Error.WriteLine("Unexpected null message with ID: " + Notes.MessageId);
                return Page();
            }

            // Unlink the original message if linking to a new message
            if(OriginalMessageId != null)
            {
                var originalMessage = await _context.Message.FindAsync(OriginalMessageId);
                if(originalMessage != null &&
                        originalMessage.NotesId != message.NotesId)
                {
                    originalMessage.NotesId = null;
                    _context.Update(originalMessage);
                }
            }

            message.NotesId = Notes.Id;
            _context.Update(Notes);
            _context.Update(message);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogCritical($"User {User.Identity.Name} edited object with new values'{Notes.ToString()}.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotesExists(Notes.Id))
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

        private bool NotesExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
