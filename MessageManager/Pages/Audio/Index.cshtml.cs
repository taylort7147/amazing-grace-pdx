using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Models;

namespace MessageManager.Pages_Audio
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly MessageContext _context;

        public IndexModel(MessageContext context)
        {
            _context = context;
        }

        public IList<Audio> Audio { get; set; }

        public async Task OnGetAsync()
        {
            Audio = await _context.Audio
                    .Include(a => a.Message).ToListAsync();
        }
    }
}