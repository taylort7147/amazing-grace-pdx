using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageManager.Data;
using MessageManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace MessageManager.Pages.Playlists
{
    public class CreateModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;
        private readonly ILogger _logger;

        public CreateModel(MessageManager.Data.MessageContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet(int seriesId)
        {
            // Only show series that don't have a linked playlist reference
            var unlinkedSeriesList = _context.Series.Where(s => s.PlaylistId == null);

            var unlinkedSeriesSelectList = new SelectList(unlinkedSeriesList, "Id", "Name");
            var selected = unlinkedSeriesSelectList.Where(x => x.Value == seriesId.ToString()).FirstOrDefault();
            if (selected != null)
            {
                selected.Selected = true;
            }
            ViewData["SeriesId"] = unlinkedSeriesSelectList;
            return Page();
        }

        [BindProperty]
        public Playlist Playlist { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var series = await _context.Series.FindAsync(Playlist.SeriesId);
            if (series == null)
            {
                Console.Error.WriteLine("Unexpected null series with ID: " + Playlist.SeriesId);
                return Page();
            }

            _context.Playlist.Add(Playlist);
            await _context.SaveChangesAsync();

            // Update the series' playlist reference
            series.PlaylistId = Playlist.Id;
            _context.Series.Update(series);
            await _context.SaveChangesAsync();
            _logger.LogCritical($"User '{User.Identity.Name}' created '{Playlist.ToString()}'.");

            return RedirectToPage("/Series/Edit", new { id = series.Id });
        }
    }
}
