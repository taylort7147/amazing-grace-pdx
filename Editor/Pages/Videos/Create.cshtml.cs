using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Editor.Models;

namespace Editor.Pages_Videos
{
    public class CreateModel : PageModel
    {
        private readonly MessageContext _context;

        public CreateModel(MessageContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int messageId)
        {
            // Only show messages that don't have a linked audio reference
            var unlinkedMessages = _context.Message.Where(m => m.VideoId == null);

            var unlinkedMessageSelectList = new SelectList(unlinkedMessages, "Id", "Description");
            var selected = unlinkedMessageSelectList.Where(x => x.Value == messageId.ToString()).FirstOrDefault();
            if(selected != null)
            {
                selected.Selected = true;
            }
            ViewData["MessageId"] = unlinkedMessageSelectList;
            return Page();
        }

        [BindProperty]
        public Video Video { get; set; }

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

            _context.Video.Add(Video);
            await _context.SaveChangesAsync();

            // Update the message's video reference
            message.VideoId = Video.Id;
            _context.Message.Update(message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}