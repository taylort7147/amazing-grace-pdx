using System;
using System.Text.Json;
using BibleReferenceParser.Data;
using NUnit.Framework;

namespace BibleReferenceParserTests
{
    public class BibleDetailsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanGetBookDescriptions()
        {
            var descriptions = BibleDetails.BookDescriptions;
            Assert.AreEqual(66, descriptions.Length);

            {
                var book = Array.Find(descriptions, x => x.Book == BibleBook.Genesis);
                Assert.NotNull(book);
                var chapterCount = 50;
                Assert.AreEqual(chapterCount, book.VerseCountsByChapter.Count);
                Assert.AreEqual(31, book.VerseCountsByChapter[1]);
                Assert.AreEqual(26, book.VerseCountsByChapter[chapterCount]);
            }

            {
                var book = Array.Find(descriptions, x => x.Book == BibleBook.Revelation);
                Assert.NotNull(book);
                var chapterCount = 22;
                Assert.AreEqual(22, book.VerseCountsByChapter.Count);
                Assert.AreEqual(20, book.VerseCountsByChapter[1]);
                Assert.AreEqual(21, book.VerseCountsByChapter[chapterCount]);
            }
        }
    }
}