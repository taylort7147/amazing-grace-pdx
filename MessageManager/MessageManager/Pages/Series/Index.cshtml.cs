using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageManager.Data;
using MessageManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MessageManager.Pages.Series
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;

        public IndexModel(MessageManager.Data.MessageContext context)
        {
            _context = context;
        }

        public IList<MessageManager.Models.Series> Series { get; set; }

        public async Task OnGetAsync()
        {
            Series = await _context.Series
                     .Include(s => s.Messages).ToListAsync();
        }
    }
}
