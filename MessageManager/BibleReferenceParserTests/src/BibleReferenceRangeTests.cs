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
    }
}