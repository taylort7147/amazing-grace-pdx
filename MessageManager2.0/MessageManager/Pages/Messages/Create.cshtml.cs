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
using Microsoft.Extensions.Logging;

namespace MessageManager.Pages.Messages
{
    [Authorize(Policy = Constants.ReadWritePolicy)]
    public class CreateModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;
        private readonly ILogger _logger;

        public CreateModel(MessageManager.Data.MessageContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGetAsync()
        {
            var seriesSelectList = new SelectList(_context.Series, "Id", "Name");
            var selected = seriesSelectList.Where(x => x.Value == null).FirstOrDefault();
            if (selected != null)
            {
                selected.Selected = true;
            }
            ViewData["SeriesSelectList"] = seriesSelectList;
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
            _logger.LogCritical($"User '{User.Identity.Name}' created '{Message.ToString()}'.");

            return RedirectToPage("./Edit", new { id = Message.Id });
        }
    }
}
