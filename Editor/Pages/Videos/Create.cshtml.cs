using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Editor.Models;

namespace Editor.Pages_Videos
{
    public class CreateModel : PageModel
    {
        private readonly MessageContext _context;

        public CreateModel(MessageContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MessageId"] = new SelectList(_context.Message, "Id", "Description");
            return Page();
        }

        [BindProperty]
        public Video Video { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Video.Add(Video);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}