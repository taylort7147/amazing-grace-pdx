using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Data;
using MessageManager.Models;

namespace MessageManager.Pages.Notes
{
    public class DeleteModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;

        public DeleteModel(MessageManager.Data.MessageContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MessageManager.Models.Notes Notes { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Notes = await _context.Notes.FindAsync(id);

            if (Notes != null)
            {
                _context.Notes.Remove(Notes);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
