using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageManager.Areas.Identity.Authorization;
using MessageManager.Data;
using MessageManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MessageManager.Pages.Playlists
{
    [Authorize(Policy = Constants.ReadWritePolicy)]
    public class DeleteModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;
        private readonly ILogger _logger;

        public DeleteModel(MessageManager.Data.MessageContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
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
                var series = await _context.Series.FindAsync(Playlist.SeriesId);

                _context.Playlist.Remove(Playlist);

                if (series != null)
                {
                    series.PlaylistId = null;
                    _context.Series.Update(series);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Series/Edit", new { id = series.Id });
                }
                await _context.SaveChangesAsync();
                _logger.LogCritical($"User '{User.Identity.Name}' deleted '{Playlist.ToString()}'.");
            }

            return RedirectToPage("/Series/Index");
        }
    }
}
