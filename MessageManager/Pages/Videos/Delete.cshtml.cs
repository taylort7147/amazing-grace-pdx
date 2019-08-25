using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Authorization;
using MessageManager.Models;

namespace MessageManager.Pages_Videos
{
    [Authorize(Policy = Constants.ReadWritePolicy)]
    public class DeleteModel : PageModel
    {
        private readonly MessageContext _context;

        public DeleteModel(MessageContext context)
        {
            _context = context;
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
            }

            return RedirectToPage("./Index");
        }
    }
}
