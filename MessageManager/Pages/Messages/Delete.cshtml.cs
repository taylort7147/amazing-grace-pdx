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

namespace MessageManager.Pages.Messages
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
        public Message Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Message = await _context.Message.FirstOrDefaultAsync(m => m.Id == id);

            if (Message == null)
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

            Message = await _context.Message.FindAsync(id);

            if (Message != null)
            {
                if (Message.Video != null)
                {
                    _context.Video.Remove(Message.Video);
                }

                if (Message.Audio != null)
                {
                    _context.Audio.Remove(Message.Audio);
                }

                if (Message.Notes != null)
                {
                    _context.Notes.Remove(Message.Notes);
                }

                _context.Message.Remove(Message);
                await _context.SaveChangesAsync();
                _logger.LogCritical($"User '{User.Identity.Name}' deleted '{Message.ToString()}'.");
            }

            return RedirectToPage("./Index");
        }
    }
}
