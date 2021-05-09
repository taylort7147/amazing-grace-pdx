using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace BibleReferenceValidator
{
    public static class BibleDetails
    {
        private static readonly string BibleDetailsResourceName = "BibleReferenceValidator.data.bible_details.json";

        public static BookDescription[] BookDescriptions = GetBookDescriptions();

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
    }
}