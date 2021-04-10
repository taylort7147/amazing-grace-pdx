using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MessageManager.Data;
using MessageManager.Models;

namespace MessageManager.Pages.Playlists
{
    public class EditModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;

        public EditModel(MessageManager.Data.MessageContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Playlist Playlist { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Playlist = await _context.Playlist
                .Include(p => p.Series).FirstOrDefaultAsync(m => m.Id == id);

            if (Playlist == null)
            {
                return NotFound();
            }
           ViewData["SeriesId"] = new SelectList(_context.Series, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Playlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistExists(Playlist.Id))
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

        private bool PlaylistExists(int id)
        {
            return _context.Playlist.Any(e => e.Id == id);
        }
    }
}
