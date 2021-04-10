using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MessageManager.Data;
using MessageManager.Models;

namespace MessageManager.Pages.Videos
{
    public class CreateModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;

        public CreateModel(MessageManager.Data.MessageContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MessageId"] = new SelectList(_context.Message, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Video Video { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
