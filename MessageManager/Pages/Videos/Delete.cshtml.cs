using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MessageManager.Authorization;
using MessageManager.Models;

namespace MessageManager.Pages_Videos
{
    [Authorize(Policy = Constants.ReadWritePolicy)]
    public class DeleteModel : PageModel
    {
        private readonly MessageContext _context;
        private readonly ILogger _logger;

        public DeleteModel(MessageContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Video Video { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Video = await _context.Video.FindAsync(id);

            if (Video != null)
            {
                var message = await _context.Message.FindAsync(Video.MessageId);
                if(message != null)
                {
                    message.VideoId = null;
                    _context.Message.Update(message);
                }

                _context.Video.Remove(Video);
                await _context.SaveChangesAsync();
                _logger.LogCritical($"User {User.Identity.Name} deleted '{Video.ToString()}.");
            }

            return RedirectToPage("./Index");
        }
    }
}
