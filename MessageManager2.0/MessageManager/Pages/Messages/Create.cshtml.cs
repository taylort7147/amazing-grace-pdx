using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MessageManager.Data;
using MessageManager.Models;

namespace MessageManager.Pages.Messages
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
        ViewData["SeriesId"] = new SelectList(_context.Series, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Message Message { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Message.Add(Message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
