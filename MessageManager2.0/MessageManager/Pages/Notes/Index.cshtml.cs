using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Data;
using MessageManager.Models;

namespace MessageManager.Pages.Notes
{
    public class IndexModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;

        public IndexModel(MessageManager.Data.MessageContext context)
        {
            _context = context;
        }

        public IList<MessageManager.Models.Notes> Notes { get;set; }

        public async Task OnGetAsync()
        {
            Notes = await _context.Notes
                .Include(n => n.Message).ToListAsync();
        }
    }
}
