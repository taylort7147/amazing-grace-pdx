using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Data;
using MessageManager.Models;

namespace MessageManager.Pages.Videos
{
    public class DeleteModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;

        public DeleteModel(MessageManager.Data.MessageContext context)
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
                _context.Video.Remove(Video);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}