using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Data;
using MessageManager.Models;

namespace MessageManager.Pages.Audio
{
    public class DetailsModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;

        public DetailsModel(MessageManager.Data.MessageContext context)
        {
            _context = context;
        }

        public MessageManager.Models.Audio Audio { get; set; }

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
    }
}
