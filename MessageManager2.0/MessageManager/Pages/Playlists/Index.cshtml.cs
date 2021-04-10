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
    public class IndexModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;

        public IndexModel(MessageManager.Data.MessageContext context)
        {
            _context = context;
        }

        public IList<Playlist> Playlist { get;set; }

        public async Task OnGetAsync()
        {
            Playlist = await _context.Playlist
                .Include(p => p.Series).ToListAsync();
        }
    }
}
