using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MessageManager.Authorization;
using MessageManager.Models;

namespace MessageManager.Pages_Series
{
    [Authorize(Policy = Constants.ReadWritePolicy)]
    public class DeleteModel : PageModel
    {
        private readonly MessageContext _context;
        private readonly ILogger _logger;

        public DeleteModel(MessageContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Series Series { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Series = await _context.Series
                     .Include(s => s.Messages).FirstOrDefaultAsync(s => s.Id == id);

            if (Series == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Series = await _context.Series.Include(s => s.Messages).FirstOrDefaultAsync(s => s.Id == id);

            if (Series != null)
            {
                foreach(var message in Series.Messages)
                {
                    if(message != null)
                    {
                        message.SeriesId = null;
                        _context.Message.Update(message);
                    }
                }

                _context.Series.Remove(Series);
                await _context.SaveChangesAsync();
                _logger.LogCritical($"User {User.Identity.Name} deleted '{Series.ToString()}.");
            }

            return RedirectToPage("./Index");
        }
    }
}
