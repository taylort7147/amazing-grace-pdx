using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Data.Nucleus;
using MessageManager.Models.Nucleus;

namespace MessageManager.Pages.Nucleus.Notes
{
    public class DeleteModel : PageModel
    {
        private readonly MessageManager.Data.Nucleus.NucleusContext _context;

        public DeleteModel(MessageManager.Data.Nucleus.NucleusContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Nucleus.Notes Notes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes.FirstOrDefaultAsync(m => m.Id == id);

            if (notes is not null)
            {
                Notes = notes;

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

            var notes = await _context.Notes.FindAsync(id);
            if (notes != null)
            {
                Notes = notes;
                _context.Notes.Remove(Notes);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
