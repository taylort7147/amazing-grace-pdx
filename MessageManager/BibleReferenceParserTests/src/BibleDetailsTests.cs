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

        [Test]
        public void IsValidBibleReferenceValidBook()
        {
            var reference = new BibleReference { Book = BibleBook.Exodus };
            Assert.IsTrue(BibleDetails.IsValidBibleReference(reference));
        }

        [Test]
        public void IsValidBibleReferenceInvalidBook()
        {
            var reference = new BibleReference { Book = (BibleBook)(-1) };
            Assert.IsFalse(BibleDetails.IsValidBibleReference(reference));
        }

        [Test]
        public void IsValidBibleReferenceValidBookValidChapter()
        {
            var reference = new BibleReference { Book = BibleBook.Exodus, Chapter = 40 };
            Assert.IsTrue(BibleDetails.IsValidBibleReference(reference));
        }

        [Test]
        public void IsValidBibleReferenceValidBookInvalidChapterTooSmall()
        {
            var reference = new BibleReference { Book = BibleBook.Exodus, Chapter = 0 };
            Assert.IsFalse(BibleDetails.IsValidBibleReference(reference));
        }

        [Test]
        public void IsValidBibleReferenceValidBookInvalidChapterTooLarge()
        {
            var reference = new BibleReference { Book = BibleBook.Exodus, Chapter = 41 };
            Assert.IsFalse(BibleDetails.IsValidBibleReference(reference));
        }

        [Test]
        public void IsValidBibleReferenceValidBookValidChapterValidVerse()
        {
            var reference = new BibleReference { Book = BibleBook.Exodus, Chapter = 40, Verse = 38 };
            Assert.IsTrue(BibleDetails.IsValidBibleReference(reference));
        }

        [Test]
        public void IsValidBibleReferenceValidBookValidChapterInvalidVerseTooSmall()
        {
            var reference = new BibleReference { Book = BibleBook.Exodus, Chapter = 40, Verse = 0 };
            Assert.IsFalse(BibleDetails.IsValidBibleReference(reference));
        }

        [Test]
        public void IsValidBibleReferenceValidBookValidChapterInvalidVerseTooLarge()
        {
            var reference = new BibleReference { Book = BibleBook.Exodus, Chapter = 40, Verse = 39 };
            Assert.IsFalse(BibleDetails.IsValidBibleReference(reference));
        }

        [Test]
        public void IsValidBibleReferenceRangeValidReferenceWithNoEndReference()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 40, Verse = 1 };
            var range = new BibleReferenceRange { First = refA };
            Assert.IsTrue(BibleDetails.IsValidBibleReferenceRange(range));
        }

        [Test]
        public void IsValidBibleReferenceRangeInvalidReferenceWithNoEndReference()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 40, Verse = 0 };
            var range = new BibleReferenceRange { First = refA };
            Assert.IsFalse(BibleDetails.IsValidBibleReferenceRange(range));
        }

        [Test]
        public void IsValidBibleReferenceRangeValidReferenceToSameValidReference()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 40, Verse = 1 };
            var refB = new BibleReference { Book = BibleBook.Exodus, Chapter = 40, Verse = 1 };
            var range = new BibleReferenceRange { First = refA, Last = refB };
            Assert.IsTrue(BibleDetails.IsValidBibleReferenceRange(range));
        }

        [Test]
        public void IsValidBibleReferenceRangeValidReferenceToLaterValidReference()
        {
            var refA = new BibleReference { Book = BibleBook.Genesis };
            var refB = new BibleReference { Book = BibleBook.Deuteronomy };
            var range = new BibleReferenceRange { First = refA, Last = refB };
            Assert.IsTrue(BibleDetails.IsValidBibleReferenceRange(range));
        }

        [Test]
        public void IsValidBibleReferenceRangeValidReferenceToEarlierValidReference()
        {
            var refA = new BibleReference { Book = BibleBook.Deuteronomy };
            var refB = new BibleReference { Book = BibleBook.Genesis };
            var range = new BibleReferenceRange { First = refA, Last = refB };
            Assert.IsFalse(BibleDetails.IsValidBibleReferenceRange(range));
        }

        [Test]
        public void IsValidBibleReferenceRangeValidReferenceToInvalidReference()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 40, Verse = 1 };
            var refB = new BibleReference { Book = BibleBook.Exodus, Chapter = 40, Verse = 39 };
            var range = new BibleReferenceRange { First = refA, Last = refB };
            Assert.IsFalse(BibleDetails.IsValidBibleReferenceRange(range));
        }

        [Test]
        public void IsValidBibleReferenceRangeInvalidReferenceToValidReference()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 40, Verse = 0 };
            var refB = new BibleReference { Book = BibleBook.Exodus, Chapter = 40, Verse = 38 };
            var range = new BibleReferenceRange { First = refA, Last = refB };
            Assert.IsFalse(BibleDetails.IsValidBibleReferenceRange(range));
        }

        [Test]
        public void IsValidBibleReferenceRangeInvalidReferenceToInvalidReference()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 40, Verse = 0 };
            var refB = new BibleReference { Book = BibleBook.Exodus, Chapter = 40, Verse = 39 };
            var range = new BibleReferenceRange { First = refA, Last = refB };
            Assert.IsFalse(BibleDetails.IsValidBibleReferenceRange(range));
        }
    }
}