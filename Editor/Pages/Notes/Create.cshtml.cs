using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Editor.Models;

namespace Editor.Pages_Notes
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
            var unlinkedMessages = _context.Message.Where(m => m.NotesId == null);

            var unlinkedMessageSelectList = new SelectList(unlinkedMessages, "Id", "Description");
            if(messageId != null)
            {
                var selected = unlinkedMessageSelectList.Where(x => x.Value == messageId.ToString()).First();
                selected.Selected = true;
            }
            ViewData["MessageId"] = unlinkedMessageSelectList;
            return Page();
        }

        [BindProperty]
        public Notes Notes { get; set; }

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

            _context.Notes.Add(Notes);
            await _context.SaveChangesAsync();

            // Update the message's notes reference
            message.NotesId = Notes.Id;
            _context.Message.Update(message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}