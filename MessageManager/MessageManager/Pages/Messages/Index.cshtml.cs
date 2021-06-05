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
            IQueryable<Message> messages = null;

            var searchCriteria = Search.GetCriteria(searchString);
            switch (searchCriteria.Type)
            {
                case Search.Type.FindAnywhere:
                    {
                        messages = FindAnywhere(searchCriteria.SearchString);
                    }
                    break;
                case Search.Type.FindByBibleReference:
                    {
                        messages = FindByBibleReference(searchCriteria.SearchString);
                    }
                    break;
                case Search.Type.FindByMessage:
                    {
                        messages = FindByMessage(searchCriteria.SearchString);
                    }
                    break;
                case Search.Type.FindBySeries:
                    {
                        messages = FindBySeries(searchCriteria.SearchString);
                    }
                    break;
                case Search.Type.FindAll:
                default:
                    {
                        messages = GetAllMessages();
                    }
                    break;
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
            Messages = (messages.AsEnumerable()).Distinct().ToList();
        }

        IQueryable<Message> GetAllMessages()
        {
            var messages = from m in _context.Message
                .Include(m => m.Series)
                .Include(m => m.BibleReferences)
                           select m;
            return messages;
        }

        IQueryable<Message> GetNoMessages()
        {
            var messages = from m in _context.Message
                .Where(m => false)
                           select m;
            return messages;
        }

        IQueryable<Message> FindAnywhere(string searchString)
        {
            var messages = FindByMessage(searchString)
                .Union(FindBySeries(searchString));
            if (BibleReferenceValidation.Validate(searchString) == ValidationResult.Success)
            {
                messages = messages.Union(FindByBibleReference(searchString));
            }
            return messages;
        }

        IQueryable<Message> FindByBibleReference(string searchString)
        {
            var validationResult = BibleReferenceValidation.Validate(searchString);
            var messages = GetNoMessages();

            if (validationResult != ValidationResult.Success)
            {
                ViewData["SearchErrorMessage"] = validationResult.ErrorMessage;
                return messages;
            }

            var bibleReferences = Parser.TryParse(searchString);
            if (bibleReferences != null && bibleReferences.Count == 1)
            {
                // x1 <= y2 && y1 <= x2
                var x = BibleReferenceRange.From(bibleReferences[0].GetExplicitRange());
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

                messages = GetAllMessages()
                    .Join(matchingReferences,
                        m => m.Id,
                        r => r.MessageId,
                        (m, r) => m);
                foreach (var reference in matchingReferences)
                {
                    MatchingBibleReferences.Add(reference.ToFriendlyString());
                }
            }
            return messages;
        }

        IQueryable<Message> FindByMessage(string searchString)
        {
            return GetAllMessages().Where(Search.GetMessageSearchExpression(searchString));
        }

        IQueryable<Message> FindBySeries(string searchString)
        {
            return GetAllMessages().Where(Search.GetSeriesSearchExpression(searchString));
        }
    }
}

