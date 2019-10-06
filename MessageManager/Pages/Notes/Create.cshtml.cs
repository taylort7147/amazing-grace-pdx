using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MessageManager.Models;
using MessageManager.Authorization;

namespace MessageManager.Pages_Notes
{
    [Authorize(Policy = Constants.ReadWritePolicy)]
    public class CreateModel : PageModel
    {
        private readonly MessageContext _context;
        private readonly ILogger _logger;

        public CreateModel(MessageContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet(int messageId)
        {
            // Only show messages that don't have a linked audio reference
            var unlinkedMessages = _context.Message.Where(m => m.NotesId == null);

            var unlinkedMessageSelectList = new SelectList(unlinkedMessages, "Id", "Title");
            var selected = unlinkedMessageSelectList.Where(x => x.Value == messageId.ToString()).FirstOrDefault();
            if(selected != null)
            {
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
            _logger.LogCritical($"User {User.Identity.Name} created '{Notes.ToString()}.");

            return RedirectToPage("./Index");
        }
    }
}