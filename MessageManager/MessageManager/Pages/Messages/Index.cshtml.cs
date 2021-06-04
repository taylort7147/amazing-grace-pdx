using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BibleReferenceParser.Parsing;
using MessageManager.Data;
using MessageManager.Models;
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

        private static bool Search(Message message, string searchString)
        {
            var lowerSearchString = searchString.ToLower();
            if (message.Title.ToLower().Contains(lowerSearchString))
            {
                return true;
            }
            if (message.Description.ToLower().Contains(lowerSearchString))
            {
                return true;
            }
            return false;
        }

        private static Expression<Func<Message, bool>> SearchExpression(string searchString)
        {
            searchString = searchString.ToLower();
            return m =>
                m.Title.ToLower().Contains(searchString) ||
                m.Description.ToLower().Contains(searchString) ||
                m.Series.Name.ToLower().Contains(searchString) ||
                m.Series.Description.ToLower().Contains(searchString);
        }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            ViewData["DateSortParam"] = (sortOrder == SortOrder.DateDescending) ? SortOrder.Date : SortOrder.DateDescending;
            ViewData["TitleSortParam"] = (sortOrder == SortOrder.Title) ? SortOrder.TitleDescending : SortOrder.Title;
            ViewData["DescriptionSortParam"] = (sortOrder == SortOrder.Description) ? SortOrder.DescriptionDescending : SortOrder.Description;
            ViewData["SeriesSortParam"] = (sortOrder == SortOrder.Series) ? SortOrder.SeriesDescending : SortOrder.Series;
            ViewData["CurrentSearch"] = searchString;

            var messages = from m in _context.Message
                .Include(m => m.Series)
                .Include(m => m.BibleReferences)
                           select m;
            IQueryable<Message> bibleReferenceSearchResults = null;
            MatchingBibleReferences = new List<string>();

            if (!String.IsNullOrEmpty(searchString))
            {
                var bibleReferences = Parser.TryParse(searchString);

                if (bibleReferences != null && bibleReferences.Count == 1)
                {
                    // x1 <= y2 && y1 <= x2
                    var x = BibleReferenceRange.From(bibleReferences[0].GetExplicitRange());
                    Expression<Func<BibleReferenceRange, bool>> z = r => r.StartBook < 5;
                    var matchingReferences = from y in _context.BibleReferences
                                             where (
                                                 (x.StartBook < y.EndBook) ||
                                                 (x.StartBook == y.EndBook && x.StartChapter < y.EndChapter) ||
                                                 (x.StartBook == y.EndBook && x.StartChapter == y.EndChapter && x.StartVerse <= y.EndVerse)
                                             ) &&
                                             (
                                                 (y.StartBook < x.EndBook) ||
                                                 (y.StartBook == x.EndBook && y.StartChapter < x.EndChapter) ||
                                                 (y.StartBook == x.EndBook && y.StartChapter == x.EndChapter && y.StartVerse <= x.EndVerse)
                                             )
                                             select y;
                    bibleReferenceSearchResults = messages
                        .Join(matchingReferences,
                            m => m.Id,
                            r => r.MessageId,
                            (m, r) => m);
                    foreach (var reference in matchingReferences)
                    {
                        MatchingBibleReferences.Add(reference.ToFriendlyString());
                    }
                }

                // Filter text search results
                messages = messages.Where(SearchExpression(searchString));

                // Append Bible reference results
                if (bibleReferenceSearchResults != null)
                {
                    messages = messages.Union(bibleReferenceSearchResults);
                }
            }

            switch (sortOrder)
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
                case SortOrder.Series:
                    messages = messages.OrderBy(m => m.Series.Name);
                    break;
                case SortOrder.SeriesDescending:
                    messages = messages.OrderByDescending(m => m.Series.Name);
                    break;
                case SortOrder.Date:
                    messages = messages.OrderBy(m => m.Date);
                    break;
                case SortOrder.DateDescending:
                default:
                    messages = messages.OrderByDescending(m => m.Date);
                    break;
            }
            Messages = (await messages.ToListAsync()).Distinct().ToList();
        }
    }
}

