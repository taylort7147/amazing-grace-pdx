using System.Collections.Generic;
using System.Linq;
using BibleReferenceParser.Data;
using NUnit.Framework;

namespace BibleReferenceParserTests
{
    public class HashFunctionTests
    {
        [Test]
        public void BibleReferenceGetHashCode()
        {
            var hashes = new HashSet<int>();
            var verseCount = 0;
            foreach (var description in BibleDetails.BookDescriptions)
            {
                foreach (var chapterDescription in description.VerseCountsByChapter)
                {
                    for (var verse = 1; verse <= chapterDescription.Value; ++verse)
                    {
                        var reference = new BibleReference { Book = description.Book, Chapter = chapterDescription.Key, Verse = verse };
                        int hash = reference.GetHashCode();
                        hashes.Add(hash);
                        ++verseCount;
                    }
                }
            }
            Assert.AreEqual(verseCount, hashes.Count);
        }
    }
}