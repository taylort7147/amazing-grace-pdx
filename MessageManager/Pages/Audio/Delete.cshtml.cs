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

namespace MessageManager.Pages_Audio
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
        public Audio Audio { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Audio = await _context.Audio.FindAsync(id);

            if (Audio != null)
            {
                var message = await _context.Message.FindAsync(Audio.MessageId);
                if(message != null)
                {
                    message.AudioId = null;
                    _context.Message.Update(message);
                }

                _context.Audio.Remove(Audio);
                await _context.SaveChangesAsync();
                _logger.LogCritical($"User {User.Identity.Name} deleted '{Audio.ToString()}.");
            }

            return RedirectToPage("./Index");
        }
    }
}
