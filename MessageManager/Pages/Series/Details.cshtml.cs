using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Models;

namespace MessageManager.Pages_Series
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly MessageContext _context;

        public DetailsModel(MessageContext context)
        {
            _context = context;
        }

        public Series Series { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Series = await _context.Series
                     .Include(s => s.Messages).FirstOrDefaultAsync(s => s.Id == id);

            if (Series == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
