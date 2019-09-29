using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MessageManager.Authorization;
using MessageManager.Models;

namespace MessageManager.Pages_Audio
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
            var unlinkedMessages = _context.Message.Where(m => m.AudioId == null);

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
            _logger.LogCritical($"User {User.Identity.Name} created '{Audio.ToString()}.");

            return RedirectToPage("./Index");
        }
    }
}