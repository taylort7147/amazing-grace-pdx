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
        public static BibleBook[] Books;

        static BibleDetails()
        {
            BookDescriptions = GetBookDescriptions();
            Books = GetBooks();
        }

        public static int GetLastChapterForBook(BibleBook book)
        {
            var description = BookDescriptions.First(x => x.Book == book);
            return description.VerseCountsByChapter.Keys.Max();
        }

        public static int GetLastVerseForBookChapter(BibleBook book, int chapter)
        {
            var description = BookDescriptions.First(x => x.Book == book);
            return description.VerseCountsByChapter[chapter];
        }

        public static bool IsValidBibleReference(BibleReference reference)
        {
            if (reference == null)
            {
                return false;
            }
            var description = BookDescriptions.FirstOrDefault(x => x.Book == reference.Book);
            if (description == null)
            {
                return false;
            }
            if (reference.Chapter != null)
            {
                var chapterCount = description.VerseCountsByChapter.Keys.Max();
                if (reference.Chapter < 1 || reference.Chapter > chapterCount)
                {
                    return false;
                }
                if (reference.Verse != null)
                {
                    var verseCount = description.VerseCountsByChapter[reference.Chapter.Value];
                    if (reference.Verse < 1 || reference.Verse > verseCount)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool IsValidBibleReferenceRange(BibleReferenceRange referenceRange)
        {
            if (referenceRange == null)
            {
                return false;
            }
            if (!IsValidBibleReference(referenceRange.First))
            {
                return false;
            }
            if (referenceRange.Last != null)
            {
                if (!IsValidBibleReference(referenceRange.Last))
                {
                    return false;
                }
                if (referenceRange.Last.CompareTo(referenceRange.First) < 0)
                {
                    return false;
                }
            }
            return true;
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

        private static BibleBook[] GetBooks()
        {
            var books = BookDescriptions.Select(x => x.Book).ToArray();
            return books;
        }
    }
}