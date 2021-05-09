using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageManager.Data;
using MessageManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MessageManager.Pages.Series
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;

        public DetailsModel(MessageManager.Data.MessageContext context)
        {
            _context = context;
        }

        public MessageManager.Models.Series Series { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Series = await _context.Series
                     .Include(s => s.Playlist)
                     .Include(s => s.Messages).FirstOrDefaultAsync(s => s.Id == id);

            if (Series == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
