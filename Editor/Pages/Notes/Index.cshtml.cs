using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Models;

namespace MessageManager.Pages_Notes
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly MessageContext _context;

        public IndexModel(MessageContext context)
        {
            _context = context;
        }

        public IList<Notes> Notes { get; set; }

        public async Task OnGetAsync()
        {
            Notes = await _context.Notes
                    .Include(n => n.Message).ToListAsync();
        }
    }
}
