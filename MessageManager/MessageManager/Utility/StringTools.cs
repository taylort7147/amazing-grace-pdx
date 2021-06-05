using System;
using System.Text;
using System.Text.RegularExpressions;

namespace MessageManager.Utility
{
    public static class StringTools
    {
        public static string ReplaceTransform(this string input, string oldString, Func<string, string> transformFunc, StringComparison stringComparison = StringComparison.CurrentCulture)
        {
            var index = 0;
            var sb = new StringBuilder(input);
            while (true)
            {
                index = sb.ToString().IndexOf(oldString, index, stringComparison);
                if (index == -1)
                {
                    break;
                }
                var match = sb.ToString().Substring(index, oldString.Length);
                var replacement = transformFunc(match);
                sb.Remove(index, match.Length);
                sb.Insert(index, replacement);
                index += replacement.Length;
            }
            return sb.ToString();
        }
    }
}