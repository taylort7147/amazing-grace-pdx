using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageManager.Data;
using MessageManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MessageManager.Pages.Playlists
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;

        public DetailsModel(MessageManager.Data.MessageContext context)
        {
            _context = context;
        }

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
    }
}
