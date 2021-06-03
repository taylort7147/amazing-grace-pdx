using BibleReferenceParser.Data;
using NUnit.Framework;

namespace BibleReferenceParserTests
{
    public class BibleReferenceRangeTests
    {
        [Test]
        public void CompareRangeToNull()
        {
            var range = new BibleReferenceRange { First = new BibleReference { Book = BibleBook.Genesis } };
            Assert.AreEqual(1, range.CompareTo(null));
        }

        [Test]
        public void CompareGreaterRangeToRange()
        {
            var rangeA = new BibleReferenceRange { First = new BibleReference { Book = BibleBook.Revelation } };
            var rangeB = new BibleReferenceRange { First = new BibleReference { Book = BibleBook.Genesis } };
            Assert.AreEqual(1, rangeA.CompareTo(rangeB));
        }

        [Test]
        public void CompareLessRangeToRange()
        {
            var rangeA = new BibleReferenceRange { First = new BibleReference { Book = BibleBook.Genesis } };
            var rangeB = new BibleReferenceRange { First = new BibleReference { Book = BibleBook.Revelation } };
            Assert.AreEqual(-1, rangeA.CompareTo(rangeB));
        }

        [Test]
        public void CompareEqualRangeToRange()
        {
            var rangeA = new BibleReferenceRange { First = new BibleReference { Book = BibleBook.Genesis } };
            var rangeB = new BibleReferenceRange { First = new BibleReference { Book = BibleBook.Genesis } };
            Assert.AreEqual(0, rangeA.CompareTo(rangeB));
        }

        [Test]
        public void EqualsRangeToRangeIsTrue()
        {
            var rangeA = new BibleReferenceRange { First = new BibleReference { Book = BibleBook.Genesis } };
            var rangeB = new BibleReferenceRange { First = new BibleReference { Book = BibleBook.Genesis } };
            Assert.IsTrue(rangeA.Equals(rangeB));
        }

        [Test]
        public void EqualsRangeToNullIsFalse()
        {
            var rangeA = new BibleReferenceRange { First = new BibleReference { Book = BibleBook.Genesis } };
            Assert.IsFalse(rangeA.Equals(null));
        }

        [Test]
        public void EqualsRangeToRangeIsFalse()
        {
            var rangeA = new BibleReferenceRange { First = new BibleReference { Book = BibleBook.Genesis } };
            var rangeB = new BibleReferenceRange { First = new BibleReference { Book = BibleBook.Revelation } };
            Assert.IsFalse(rangeA.Equals(rangeB));
        }

        [Test]
        public void ToFriendlyStringFirstBookLastNull()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis },
                Last = null
            };
            Assert.AreEqual("Genesis", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringFirstBookChapterLastNull()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 2 },
                Last = null
            };
            Assert.AreEqual("Genesis 2", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringPartialChapterAtStart()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 1, Verse = 1 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 1, Verse = 5 }
            };
            Assert.AreEqual("Genesis 1:1-5", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringPartialChapterAtEnd()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 1, Verse = 26 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 1, Verse = 31 }
            };
            Assert.AreEqual("Genesis 1:26-31", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringFullChapter()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 1, Verse = 1 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 1, Verse = 31 }
            };
            Assert.AreEqual("Genesis 1", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringFullChapterToPartialChapter()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 1, Verse = 1 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 3, Verse = 4 }
            };
            Assert.AreEqual("Genesis 1-3:4", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringPartialChapterToFullChapter()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 1, Verse = 26 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 3, Verse = 24 }
            };
            Assert.AreEqual("Genesis 1:26-3:24", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringFullChapterToFullChapter()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 1, Verse = 1 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 3, Verse = 24 }
            };
            Assert.AreEqual("Genesis 1-3", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringChapterToChapter()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 1 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 3 }
            };
            Assert.AreEqual("Genesis 1-3", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringChapterToFullChapter()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 1 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 3, Verse = 24 }
            };
            Assert.AreEqual("Genesis 1-3", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringFullChapterToChapter()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 1, Verse = 1 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 3 }
            };
            Assert.AreEqual("Genesis 1-3", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringPartialBookAtStartChapter()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 1 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 40 }
            };
            Assert.AreEqual("Genesis 1-40", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringPartialBookAtEndChapter()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 40 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 50 }
            };
            Assert.AreEqual("Genesis 40-50", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringPartialBookAtStartChapterVerse()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 1, Verse = 1 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 40, Verse = 3 }
            };
            Assert.AreEqual("Genesis 1-40:3", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringPartialBookAtEndChapterVerse()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 40, Verse = 2 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 50, Verse = 26 }
            };
            Assert.AreEqual("Genesis 40:2-50:26", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringFullBookChapter()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 1 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 50 }
            };
            Assert.AreEqual("Genesis", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringFullBookChapterVerse()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 1, Verse = 1 },
                Last = new BibleReference { Book = BibleBook.Genesis, Chapter = 50, Verse = 26 }
            };
            Assert.AreEqual("Genesis", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringBookToBook()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis },
                Last = new BibleReference { Book = BibleBook.Exodus }
            };
            Assert.AreEqual("Genesis-Exodus", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringBookToBookChapter()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis },
                Last = new BibleReference { Book = BibleBook.Exodus, Chapter = 2 }
            };
            Assert.AreEqual("Genesis-Exodus 2", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringBookToBookChapterVerse()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis },
                Last = new BibleReference { Book = BibleBook.Exodus, Chapter = 2, Verse = 3 }
            };
            Assert.AreEqual("Genesis-Exodus 2:3", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringBookChapterToBook()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 4 },
                Last = new BibleReference { Book = BibleBook.Exodus }
            };
            Assert.AreEqual("Genesis 4-Exodus", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringBookChapterToBookChapter()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 4 },
                Last = new BibleReference { Book = BibleBook.Exodus, Chapter = 2 }
            };
            Assert.AreEqual("Genesis 4-Exodus 2", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringBookChapterToBookChapterVerse()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 4 },
                Last = new BibleReference { Book = BibleBook.Exodus, Chapter = 2, Verse = 3 }
            };
            Assert.AreEqual("Genesis 4-Exodus 2:3", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringBookChapterVerseToBook()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 4, Verse = 5 },
                Last = new BibleReference { Book = BibleBook.Exodus }
            };
            Assert.AreEqual("Genesis 4:5-Exodus", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringBookChapterVerseToBookChapter()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 4, Verse = 5 },
                Last = new BibleReference { Book = BibleBook.Exodus, Chapter = 2 }
            };
            Assert.AreEqual("Genesis 4:5-Exodus 2", range.ToFriendlyString());
        }

        [Test]
        public void ToFriendlyStringBookChapterVerseToBookChapterVerse()
        {
            var range = new BibleReferenceRange
            {
                First = new BibleReference { Book = BibleBook.Genesis, Chapter = 4, Verse = 5 },
                Last = new BibleReference { Book = BibleBook.Exodus, Chapter = 2, Verse = 3 }
            };
            Assert.AreEqual("Genesis 4:5-Exodus 2:3", range.ToFriendlyString());
        }
    }
}