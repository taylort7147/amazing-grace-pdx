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

namespace MessageManager.Pages.Nucleus.Sermons
{
    public class EditModel : PageModel
    {
        private readonly MessageManager.Data.Nucleus.NucleusContext _context;

        public EditModel(MessageManager.Data.Nucleus.NucleusContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sermon Sermon { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sermon =  await _context.Sermons.FirstOrDefaultAsync(m => m.Id == id);
            if (sermon == null)
            {
                return NotFound();
            }
            Sermon = sermon;
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

            _context.Attach(Sermon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SermonExists(Sermon.Id))
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

        private bool SermonExists(int id)
        {
            return _context.Sermons.Any(e => e.Id == id);
        }
    }
}
