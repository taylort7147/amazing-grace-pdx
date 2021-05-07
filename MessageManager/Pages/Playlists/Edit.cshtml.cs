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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MessageManager.Pages.Playlists
{
    [Authorize(Policy = Constants.ReadWritePolicy)]
    public class EditModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;
        private readonly ILogger _logger;

        public EditModel(MessageManager.Data.MessageContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Playlist Playlist { get; set; }

        [BindProperty]
        public int? OriginalSeriesId { get; set; }

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

            OriginalSeriesId = Playlist.SeriesId;

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

            var series = await _context.Series.FindAsync(Playlist.SeriesId);
            if (series == null)
            {
                Console.Error.WriteLine("Unexpected null message with ID: " + Playlist.SeriesId);
                return Page();
            }

            // Unlink the original series if linking to a new series
            if (OriginalSeriesId != null)
            {
                var originalSeries = await _context.Series.FindAsync(OriginalSeriesId);
                if (originalSeries != null &&
                        originalSeries.PlaylistId != series.PlaylistId)
                {
                    originalSeries.PlaylistId = null;
                    _context.Update(originalSeries);
                }
            }

            series.PlaylistId = Playlist.Id;
            _context.Update(Playlist);
            _context.Update(series);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogCritical($"User '{User.Identity.Name}' edited object with new values'{Playlist.ToString()}'.");
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

            return RedirectToPage("./Details", new { id = Playlist.Id });
        }

        private bool PlaylistExists(int id)
        {
            return _context.Playlist.Any(e => e.Id == id);
        }
    }
}
