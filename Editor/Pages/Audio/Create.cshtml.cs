using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Editor.Models;

namespace Editor.Pages_Audio
{
    public class CreateModel : PageModel
    {
        private readonly MessageContext _context;

        public CreateModel(MessageContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? messageId)
        {

            // Only show messages that don't have a linked audio reference
            var unlinkedMessages = _context.Message.Where(m => m.AudioId == null);

            var unlinkedMessageSelectList = new SelectList(unlinkedMessages, "Id", "Description");
            if(messageId != null)
            {
                var selected = unlinkedMessageSelectList.Where(x => x.Value == messageId.ToString()).FirstOrDefault();
                if(selected != null)
                {
                    selected.Selected = true;
                }
            }
            ViewData["MessageId"] = unlinkedMessageSelectList;
            return Page();
        }

        [BindProperty]
        public Audio Audio { get; set; }

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

            _context.Audio.Add(Audio);
            await _context.SaveChangesAsync();

            // Update the message's audio reference
            message.AudioId = Audio.Id;
            _context.Message.Update(message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}