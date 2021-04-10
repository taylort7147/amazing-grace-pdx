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
    public class DetailsModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;

        public DetailsModel(MessageManager.Data.MessageContext context)
        {
            _context = context;
        }

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
    }
}
