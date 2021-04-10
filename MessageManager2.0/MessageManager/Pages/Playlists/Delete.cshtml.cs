using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Data;
using MessageManager.Models;

namespace MessageManager.Pages.Playlists
{
    public class DeleteModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;

        public DeleteModel(MessageManager.Data.MessageContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Playlist = await _context.Playlist.FindAsync(id);

            if (Playlist != null)
            {
                _context.Playlist.Remove(Playlist);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
