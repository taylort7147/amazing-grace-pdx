using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MessageManager.Authorization;
using MessageManager.Models;

namespace MessageManager.Pages_Messages
{
    [Authorize(Policy = Constants.ReadWritePolicy)]
    public class CreateModel : PageModel
    {
        private readonly MessageContext _context;
        private readonly ILogger _logger;

        public CreateModel(MessageContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGetAsync()
        {
            var seriesSelectList = new SelectList(_context.Series, "Id", "Name");
            var selected = seriesSelectList.Where(x => x.Value == null).FirstOrDefault();
            if(selected != null)
            {
                selected.Selected = true;
            }
            ViewData["SeriesSelectList"] = seriesSelectList;
            return Page();
        }

        [BindProperty]
        public Message Message { get; set; }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Message.Add(Message);
            await _context.SaveChangesAsync();
            _logger.LogCritical($"User {User.Identity.Name} created '{Message.ToString()}.");

            return RedirectToPage("./Edit/", new {id = Message.Id});
        }
    }
}