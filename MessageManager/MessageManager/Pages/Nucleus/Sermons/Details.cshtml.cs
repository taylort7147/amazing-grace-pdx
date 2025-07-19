using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Data.Nucleus;
using MessageManager.Models.Nucleus;

namespace MessageManager.Pages.Nucleus.Sermons
{
    public class DetailsModel : PageModel
    {
        private readonly MessageManager.Data.Nucleus.NucleusContext _context;

        public DetailsModel(MessageManager.Data.Nucleus.NucleusContext context)
        {
            _context = context;
        }

        public Sermon Sermon { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sermon = await _context.Sermons.FirstOrDefaultAsync(m => m.Id == id);

            if (sermon is not null)
            {
                Sermon = sermon;

                return Page();
            }

            return NotFound();
        }
    }
}
