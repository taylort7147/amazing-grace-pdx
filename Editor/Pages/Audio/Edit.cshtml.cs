using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Editor.Authorization;
using Editor.Models;

namespace Editor.Pages_Audio
{
    [Authorize(Policy = Constants.ReadWritePolicy)]
    public class EditModel : PageModel
    {
        private readonly MessageContext _context;

        public EditModel(MessageContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Audio Audio { get; set; }

        [BindProperty]
        public int? OriginalMessageId { get; set; }

        public SelectList MessageIdList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Audio = await _context.Audio
                    .Include(a => a.Message).FirstOrDefaultAsync(m => m.Id == id);

            if (Audio == null)
            {
                return NotFound();
            }

            OriginalMessageId = Audio.MessageId;

            // Only show messages that don't have a linked audio reference, or are already linked to this
            var selectableMessages = _context.Message.Where(m => m.AudioId == null || m.AudioId == Audio.Id);

            MessageIdList = new SelectList(selectableMessages, "Id", "Description");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var message = await _context.Message.FindAsync(Audio.MessageId);
            if(message == null)
            {
                Console.Error.WriteLine("Unexpected null message with ID: " + Audio.MessageId);
                return Page();
            }

            // Unlink the original message if linking to a new message
            if(OriginalMessageId != null)
            {
                var originalMessage = await _context.Message.FindAsync(OriginalMessageId);
                if(originalMessage != null &&
                        originalMessage.AudioId != message.AudioId)
                {
                    originalMessage.AudioId = null;
                    _context.Update(originalMessage);
                }
            }

            message.AudioId = Audio.Id;
            _context.Update(Audio);
            _context.Update(message);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AudioExists(Audio.Id))
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

        private bool AudioExists(int id)
        {
            return _context.Audio.Any(e => e.Id == id);
        }
    }
}
