using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using BibleReferenceParser.Parsing;
using MessageManager.Data;
using MessageManager.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageManager.Utility
{
    public static class MessageSearch
    {
        
        public class Criteria
        {
            public Type Type { get; set; }
            public string SearchString { get; set; }
        }

        public enum Type
        {
            FindAll,
            FindAnywhere,
            FindByBibleReference,
            FindByMessage,
            FindBySeries
        };

        public class Result
        {
            public bool Success { get; set; }
            public IEnumerable<string> Errors { get; set; }
            public IQueryable<Message> Messages { get; set; }
            public IEnumerable<string> MatchingBibleReferences { get; set; }

            public Result()
            {
                Success = true;
                Errors = new List<string>();
                MatchingBibleReferences = new List<string>();
            }
            public Result CombineStatus(Result other)
            {
                Success = Success && other.Success;
                Errors = Errors.Union(other.Errors);
                return this;
            }

            public Result Union(Result other)
            {
                CombineStatus(other);
                Messages = Messages.Union(other.Messages);
                MatchingBibleReferences = MatchingBibleReferences.Union(other.MatchingBibleReferences);
                return this;
            }
        }

        public static Type ToType(this string tagString)
        {
            var stringComparison = StringComparison.OrdinalIgnoreCase;
            if (tagString.StartsWith("b:", stringComparison))
            {
                return Type.FindByBibleReference;
            }
            if (tagString.StartsWith("m:", stringComparison))
            {
                return Type.FindByMessage;
            }
            if (tagString.StartsWith("s:", stringComparison))
            {
                return Type.FindBySeries;
            }
            return Type.FindAnywhere;
        }

        public static Criteria GetCriteria(string searchStringRaw)
        {
            if (string.IsNullOrWhiteSpace(searchStringRaw))
            {
                return new Criteria { Type = Type.FindAll, SearchString = null };
            }
            if (searchStringRaw.Length < 2 || searchStringRaw[1] != ':')
            {
                return new Criteria { Type = Type.FindAnywhere, SearchString = searchStringRaw };
            }

            var tag = searchStringRaw.Substring(0, 2);
            var searchString = searchStringRaw.Substring(2);
            var type = tag.ToType();
            return new Criteria { Type = type, SearchString = searchString };
        }

        private static Expression<Func<Message, bool>> GetMessageSearchExpression(string searchString)
        {
            searchString = searchString.ToLower();
            return m =>
                m.Title.ToLower().Contains(searchString) ||
                m.Description.ToLower().Contains(searchString);
        }

        private static Expression<Func<Message, bool>> GetSeriesSearchExpression(string searchString)
        {
            searchString = searchString.ToLower();
            return m =>
                m.Series.Name.ToLower().Contains(searchString) ||
                m.Series.Description.ToLower().Contains(searchString);
        }

        private static IQueryable<Message> GetAllMessages(in MessageContext context)
        {
            var messages = from m in context.Message
                .Include(m => m.Series)
                .Include(m => m.BibleReferences)
                           select m;
            return messages;
        }

        private static IQueryable<Message> GetNoMessages(in MessageContext context)
        {
            var messages = from m in context.Message
                .Where(m => false)
                           select m;
            return messages;
        }

        public static Result Search(in MessageContext context, Criteria criteria)
        {
            var result = new Result();
            switch (criteria.Type)
            {
                case Type.FindAnywhere:
                        return FindAnywhere(context, criteria.SearchString);
                case Type.FindByBibleReference:
                        return FindByBibleReference(context, criteria.SearchString);
                case Type.FindByMessage:
                        return FindByMessage(context, criteria.SearchString);
                case Type.FindBySeries:
                        return FindBySeries(context, criteria.SearchString);
                case Type.FindAll:
                default:
                        return FindAll(context);
            }
        }

        public static Result FindAll(in MessageContext context)
        {
            var result = new Result();
            result.Messages = GetAllMessages(context);
            return result;
        }

        public static Result FindAnywhere(in MessageContext context, string searchString)
        {
            var result = FindByMessage(context, searchString);
            result.Union(FindBySeries(context, searchString));
            if (BibleReferenceValidation.Validate(searchString) == ValidationResult.Success)
            {
                result.Union(FindByBibleReference(context, searchString));
            }
            return result;
        }

        public static Result FindByBibleReference(in MessageContext context, string searchString)
        {
            var result = new Result();
            var validationResult = BibleReferenceValidation.Validate(searchString);
            if (validationResult != ValidationResult.Success)
            {
                result.Messages = GetNoMessages(context);
                ((List<string>)result.Errors).Add(validationResult.ErrorMessage);
                return result;
            }

            var bibleReferences = Parser.TryParse(searchString);
            if (bibleReferences != null && bibleReferences.Count == 1)
            {
                // x1 <= y2 && y1 <= x2
                var x = BibleReferenceRange.From(bibleReferences[0].GetExplicitRange());
                var matchingReferences = from y in context.BibleReferences
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

                result.Messages = GetAllMessages(context)
                    .Join(matchingReferences,
                        m => m.Id,
                        r => r.MessageId,
                        (m, r) => m);
                foreach (var reference in matchingReferences)
                {
                    ((List<string>)result.MatchingBibleReferences).Add(reference.ToFriendlyString());
                }
            }
            return result;
        }

        public static Result FindByMessage(in MessageContext context, string searchString)
        {
            var result = new Result();
            result.Messages = GetAllMessages(context).Where(GetMessageSearchExpression(searchString));
            return result;
        }

        public static Result FindBySeries(in MessageContext context, string searchString)
        {
            var result = new Result();
            result.Messages = GetAllMessages(context).Where(GetSeriesSearchExpression(searchString));
            return result;
        }
    }
}