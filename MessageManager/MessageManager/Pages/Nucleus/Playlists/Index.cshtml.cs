using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Data.Nucleus;
using MessageManager.Models.Nucleus;

namespace MessageManager.Pages.Nucleus.Playlists
{
    public class IndexModel : PageModel
    {
        private readonly MessageManager.Data.Nucleus.NucleusContext _context;

        public IndexModel(MessageManager.Data.Nucleus.NucleusContext context)
        {
            _context = context;
        }

        public IList<Playlist> Playlist { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Playlist = await _context.Playlists.ToListAsync();
        }
    }
}
