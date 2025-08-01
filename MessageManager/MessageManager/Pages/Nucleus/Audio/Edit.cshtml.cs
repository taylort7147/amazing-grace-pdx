using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MessageManager.Data.Nucleus;
using MessageManager.Models.Nucleus;

namespace MessageManager.Pages.Nucleus.Audio
{
    public class EditModel : PageModel
    {
        private readonly MessageManager.Data.Nucleus.NucleusContext _context;

        public EditModel(MessageManager.Data.Nucleus.NucleusContext context)
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

            var audio =  await _context.Audio.FirstOrDefaultAsync(m => m.Id == id);
            if (audio == null)
            {
                return NotFound();
            }
            Audio = audio;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Audio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AudioExists(Audio.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AudioExists(int id)
        {
            return _context.Audio.Any(e => e.Id == id);
        }
    }
}
