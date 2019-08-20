using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Editor.Models;

namespace Editor.Pages_Messages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        public class SortOrder
        {
            public const string Date = "date";
            public const string DateDescending = "date_desc";
            public const string Title = "title";
            public const string TitleDescending = "title_desc";
            public const string Description = "description";
            public const string DescriptionDescending = "description_desc";
        }

        private readonly MessageContext _context;

        public IndexModel(MessageContext context)
        {
            _context = context;
        }

        public IList<Message> Message { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            ViewData["DateSortParam"] = (sortOrder == SortOrder.DateDescending) ? SortOrder.Date : SortOrder.DateDescending;
            ViewData["TitleSortParam"] = (sortOrder == SortOrder.Title) ? SortOrder.TitleDescending : SortOrder.Title;
            ViewData["DescriptionSortParam"] = (sortOrder == SortOrder.Description) ? SortOrder.DescriptionDescending : SortOrder.Description;
            var messages = from m in _context.Message select m;
            switch(sortOrder)
            {

            case SortOrder.Title:
                messages = messages.OrderBy(m => m.Title);
                break;
            case SortOrder.TitleDescending:
                messages = messages.OrderByDescending(m => m.Title);
                break;

            case SortOrder.Description:
                messages = messages.OrderBy(m => m.Description);
                break;
            case SortOrder.DescriptionDescending:
                messages = messages.OrderByDescending(m => m.Description);
                break;
            case SortOrder.Date:
                messages = messages.OrderBy(m => m.Date);
                break;
            case SortOrder.DateDescending:
            default:
                messages = messages.OrderByDescending(m=>m.Date);
                break;
            }
            Message = await messages.ToListAsync();
        }
    }
}
