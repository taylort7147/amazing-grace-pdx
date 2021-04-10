using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MessageManager.Data;
using MessageManager.Models;

namespace MessageManager.Pages.Notes
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
        public MessageManager.Models.Notes Notes { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Notes.Add(Notes);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
