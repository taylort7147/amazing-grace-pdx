using BibleReferenceParser.Data;
using NUnit.Framework;

namespace BibleReferenceParserTests
{
    public class BibleReferenceTests
    {
        [Test]
        public void CompareBookToNull()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus };
            Assert.AreEqual(1, refA.CompareTo(null));
        }

        [Test]
        public void CompareGreaterBookToBook()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus };
            var refB = new BibleReference { Book = BibleBook.Genesis };
            Assert.AreEqual(1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareGreaterBookToBookChapter()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus };
            var refB = new BibleReference { Book = BibleBook.Genesis, Chapter = 5 };
            Assert.AreEqual(1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareGreaterBookToBookChapterVerse()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus };
            var refB = new BibleReference { Book = BibleBook.Genesis, Chapter = 5, Verse = 12 };
            Assert.AreEqual(1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareLessBookToBook()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus };
            var refB = new BibleReference { Book = BibleBook.Leviticus };
            Assert.AreEqual(-1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareLessBookToBookChapter()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus };
            var refB = new BibleReference { Book = BibleBook.Leviticus, Chapter = 1 };
            Assert.AreEqual(-1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareLessBookToBookChapterVerse()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus };
            var refB = new BibleReference { Book = BibleBook.Leviticus, Chapter = 1, Verse = 1 };
            Assert.AreEqual(-1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareEqualBookToBook()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus };
            var refB = new BibleReference { Book = BibleBook.Exodus };
            Assert.AreEqual(0, refA.CompareTo(refB));
        }

        [Test]
        public void CompareGreaterBookChapterToBook()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5 };
            var refB = new BibleReference { Book = BibleBook.Genesis };
            Assert.AreEqual(1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareGreaterBookChapterToBookChapter()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5 };
            var refB = new BibleReference { Book = BibleBook.Exodus, Chapter = 4 };
            Assert.AreEqual(1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareGreaterBookChapterToBookChapterVerse()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5 };
            var refB = new BibleReference { Book = BibleBook.Genesis, Chapter = 4, Verse = 12 };
            Assert.AreEqual(1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareLessBookChapterToBook()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5 };
            var refB = new BibleReference { Book = BibleBook.Leviticus };
            Assert.AreEqual(-1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareLessBookChapterToBookChapter()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5 };
            var refB = new BibleReference { Book = BibleBook.Exodus, Chapter = 6 };
            Assert.AreEqual(-1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareLessBookChapterToBookChapterVerse()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5 };
            var refB = new BibleReference { Book = BibleBook.Exodus, Chapter = 5, Verse = 1 };
            Assert.AreEqual(-1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareEqualBookChapterToBookChapter()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5 };
            var refB = new BibleReference { Book = BibleBook.Exodus, Chapter = 5 };
            Assert.AreEqual(0, refA.CompareTo(refB));
        }

        [Test]
        public void CompareGreaterBookChapterVerseToBook()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5, Verse = 11 };
            var refB = new BibleReference { Book = BibleBook.Genesis };
            Assert.AreEqual(1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareGreaterBookChapterVerseToBookChapter()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5, Verse = 11 };
            var refB = new BibleReference { Book = BibleBook.Exodus, Chapter = 5 };
            Assert.AreEqual(1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareGreaterBookChapterVerseToBookChapterVerse()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5, Verse = 11 };
            var refB = new BibleReference { Book = BibleBook.Genesis, Chapter = 5, Verse = 10 };
            Assert.AreEqual(1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareLessBookChapterVerseToBook()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5, Verse = 11 };
            var refB = new BibleReference { Book = BibleBook.Leviticus };
            Assert.AreEqual(-1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareLessBookChapterVerseToBookChapter()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5, Verse = 11 };
            var refB = new BibleReference { Book = BibleBook.Exodus, Chapter = 6 };
            Assert.AreEqual(-1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareLessBookChapterVerseToBookChapterVerse()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5, Verse = 11 };
            var refB = new BibleReference { Book = BibleBook.Exodus, Chapter = 5, Verse = 12 };
            Assert.AreEqual(-1, refA.CompareTo(refB));
        }

        [Test]
        public void CompareEqualBookChapterVerseToBookChapterVerse()
        {
            var refA = new BibleReference { Book = BibleBook.Exodus, Chapter = 5, Verse = 11 };
            var refB = new BibleReference { Book = BibleBook.Exodus, Chapter = 5, Verse = 11 };
            Assert.AreEqual(0, refA.CompareTo(refB));
        }
    }
}