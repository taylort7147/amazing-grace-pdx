using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace BibleReferenceParser.Data
{
    public static class BibleDetails
    {
        private static readonly string BibleDetailsResourceName = "BibleReferenceParser.Embedded.bible_details.json";

        public static BookDescription[] BookDescriptions;
        public static string[] Books;

        static BibleDetails()
        {
            BookDescriptions = GetBookDescriptions();
            Books = GetBooks();
        }

        private static BookDescription[] GetBookDescriptions()
        {
            var assembly = Assembly.GetAssembly(typeof(BibleDetails));
            var resourceNames = assembly.GetManifestResourceNames();
            if (!Array.Exists(resourceNames, s => s == BibleDetailsResourceName))
            {
                throw new FileLoadException(
                    $"The resource {BibleDetailsResourceName} was not found in the assembly {assembly.GetName()}");
            }

            var resourceStream = assembly.GetManifestResourceStream(BibleDetailsResourceName);
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                var bibleStatsJson = reader.ReadToEnd();
                var bibleStats = JsonSerializer.Deserialize<BookDescription[]>(bibleStatsJson);
                return bibleStats;
            }
        }

        private static string[] GetBooks()
        {
            var books = BookDescriptions.Select(x => x.Name).ToArray();
            return books;
        }
    }
}