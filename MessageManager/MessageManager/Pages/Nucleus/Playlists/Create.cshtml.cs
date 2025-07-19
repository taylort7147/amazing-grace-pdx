using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MessageManager.Data.Nucleus;
using MessageManager.Models.Nucleus;

namespace MessageManager.Pages.Nucleus.Playlists
{
    public class CreateModel : PageModel
    {
        private readonly MessageManager.Data.Nucleus.NucleusContext _context;

        public CreateModel(MessageManager.Data.Nucleus.NucleusContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Playlist Playlist { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Playlists.Add(Playlist);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
