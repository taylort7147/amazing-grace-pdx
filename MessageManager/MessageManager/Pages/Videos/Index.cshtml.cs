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

namespace MessageManager.Pages.Videos
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly MessageManager.Data.MessageContext _context;

        public IndexModel(MessageManager.Data.MessageContext context)
        {
            _context = context;
        }

        public IList<Video> Video { get; set; }

        public async Task OnGetAsync()
        {
            Video = await _context.Video
                .Include(v => v.Message).ToListAsync();
        }
    }
}
