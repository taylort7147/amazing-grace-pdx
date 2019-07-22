using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Editor.Models;

namespace Editor.Pages_Notes
{
    public class EditModel : PageModel
    {
        private readonly MessageContext _context;

        public EditModel(MessageContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Notes Notes { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Notes = await _context.Notes
                .Include(n => n.Message).FirstOrDefaultAsync(m => m.Id == id);

            if (Notes == null)
            {
                return NotFound();
            }
           ViewData["MessageId"] = new SelectList(_context.Message, "Id", "Description");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Notes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotesExists(Notes.Id))
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

        private bool NotesExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
