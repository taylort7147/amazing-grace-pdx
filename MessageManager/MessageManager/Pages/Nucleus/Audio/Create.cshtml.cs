using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MessageManager.Data.Nucleus;
using MessageManager.Models.Nucleus;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MessageManager.Pages.Nucleus.Audio
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
        public Models.Nucleus.Audio Audio { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Audio.Add(Audio);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
