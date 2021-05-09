using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageManager.Areas.Identity.Authorization;
using MessageManager.Data;
using MessageManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MessageManager.Pages.Notes
{
    [Authorize(Policy = Constants.ReadWritePolicy)]
    public class DeleteModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;
        private readonly ILogger _logger;

        public DeleteModel(MessageManager.Data.MessageContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public MessageManager.Models.Notes Notes { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Notes = await _context.Notes.FindAsync(id);

            if (Notes != null)
            {
                var message = await _context.Message.FindAsync(Notes.MessageId);

                _context.Notes.Remove(Notes);
                if (message != null)
                {
                    message.NotesId = null;
                    _context.Message.Update(message);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Messages/Edit", new { id = message.Id });
                }
                await _context.SaveChangesAsync();
                _logger.LogCritical($"User '{User.Identity.Name}' deleted '{Notes.ToString()}'.");
            }

            return RedirectToPage("/Messages/Index");
        }
    }
}
