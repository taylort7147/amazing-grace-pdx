using System;
using System.Linq.Expressions;
using MessageManager.Models;

namespace MessageManager.Pages.Messages
{
    public static class Search
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

        private static Type ToType(this string tagString)
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

        public static Expression<Func<Message, bool>> GetMessageSearchExpression(string searchString)
        {
            searchString = searchString.ToLower();
            return m =>
                m.Title.ToLower().Contains(searchString) ||
                m.Description.ToLower().Contains(searchString);
        }

        public static Expression<Func<Message, bool>> GetSeriesSearchExpression(string searchString)
        {
            searchString = searchString.ToLower();
            return m =>
                m.Series.Name.ToLower().Contains(searchString) ||
                m.Series.Description.ToLower().Contains(searchString);
        }

    }
}