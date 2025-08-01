using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Data.Nucleus;
using MessageManager.Models.Nucleus;

namespace MessageManager.Pages.Nucleus.Audio
{
    public class DeleteModel : PageModel
    {
        private readonly MessageManager.Data.Nucleus.NucleusContext _context;

        public DeleteModel(MessageManager.Data.Nucleus.NucleusContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Nucleus.Audio Audio { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audio = await _context.Audio.FirstOrDefaultAsync(m => m.Id == id);

            if (audio is not null)
            {
                Audio = audio;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audio = await _context.Audio.FindAsync(id);
            if (audio != null)
            {
                Audio = audio;
                _context.Audio.Remove(Audio);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
