using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Data;
using MessageManager.Models;

namespace MessageManager.Pages.Series
{
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

            Series = await _context.Series.FirstOrDefaultAsync(m => m.Id == id);

            if (Series == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
