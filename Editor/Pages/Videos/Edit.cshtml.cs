using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Editor.Models;

namespace Editor.Pages_Videos
{
    public class EditModel : PageModel
    {
        private readonly MessageContext _context;

        public EditModel(MessageContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Video Video { get; set; }

        [BindProperty]
        public int? OriginalMessageId { get; set; }

        public SelectList MessageIdList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Video = await _context.Video
                    .Include(v => v.Message).FirstOrDefaultAsync(m => m.Id == id);

            if (Video == null)
            {
                return NotFound();
            }

            OriginalMessageId = Video.MessageId;

            // Only show messages that don't have a linked video reference, or are already linked to this
            var selectableMessages = _context.Message.Where(m => m.VideoId == null || m.VideoId == Video.Id);

            MessageIdList = new SelectList(selectableMessages, "Id", "Description");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var message = await _context.Message.FindAsync(Video.MessageId);
            if(message == null)
            {
                Console.Error.WriteLine("Unexpected null message with ID: " + Video.MessageId);
                return Page();
            }

            // Unlink the original message if linking to a new message
            if(OriginalMessageId != null)
            {
                var originalMessage = await _context.Message.FindAsync(OriginalMessageId);
                if(originalMessage != null &&
                        originalMessage.VideoId != message.VideoId)
                {
                    originalMessage.VideoId = null;
                    _context.Update(originalMessage);
                }
            }

            message.VideoId = Video.Id;
            _context.Update(Video);
            _context.Update(message);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(Video.Id))
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

        private bool VideoExists(int id)
        {
            return _context.Video.Any(e => e.Id == id);
        }
    }
}
