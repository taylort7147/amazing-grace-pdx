using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BibleReferenceParser.Parsing;
using MessageManager.Data;
using MessageManager.Models;
using MessageManager.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MessageManager.Pages.Messages
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
            public const string Series = "series";
            public const string SeriesDescending = "series_desc";
        }

        private readonly MessageContext _context;

        public IndexModel(MessageContext context)
        {
            _context = context;
        }

        public IList<Message> Messages { get; set; }

        public IList<string> MatchingBibleReferences { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            ViewData["DateSortParam"] = (sortOrder == SortOrder.DateDescending) ? SortOrder.Date : SortOrder.DateDescending;
            ViewData["TitleSortParam"] = (sortOrder == SortOrder.Title) ? SortOrder.TitleDescending : SortOrder.Title;
            ViewData["DescriptionSortParam"] = (sortOrder == SortOrder.Description) ? SortOrder.DescriptionDescending : SortOrder.Description;
            ViewData["SeriesSortParam"] = (sortOrder == SortOrder.Series) ? SortOrder.SeriesDescending : SortOrder.Series;
            ViewData["CurrentSearch"] = searchString;
            ViewData["SearchErrorMessage"] = null;
            Messages = new List<Message>();
            MatchingBibleReferences = new List<string>();

            var searchCriteria = MessageManager.Utility.MessageSearch.GetCriteria(searchString);
            var result = MessageSearch.Search(_context, searchCriteria);

            if(!result.Success)
            {
                ViewData["SearchErrorMessage"] = string.Join("<br>", result.Errors);
                return;
            }

            switch (sortOrder)
            {
                case SortOrder.Title:
                    result.Messages = result.Messages.OrderBy(m => m.Title);
                    break;
                case SortOrder.TitleDescending:
                    result.Messages = result.Messages.OrderByDescending(m => m.Title);
                    break;
                case SortOrder.Description:
                    result.Messages = result.Messages.OrderBy(m => m.Description);
                    break;
                case SortOrder.DescriptionDescending:
                    result.Messages = result.Messages.OrderByDescending(m => m.Description);
                    break;
                case SortOrder.Series:
                    result.Messages = result.Messages.OrderBy(m => m.Series.Name);
                    break;
                case SortOrder.SeriesDescending:
                    result.Messages = result.Messages.OrderByDescending(m => m.Series.Name);
                    break;
                case SortOrder.Date:
                    result.Messages = result.Messages.OrderBy(m => m.Date);
                    break;
                case SortOrder.DateDescending:
                default:
                    result.Messages = result.Messages.OrderByDescending(m => m.Date);
                    break;
            }
            MatchingBibleReferences = result.MatchingBibleReferences.ToList();
            Messages = (await result.Messages.ToListAsync()).Distinct().ToList();
        }
    }
}

