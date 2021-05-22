using System;
using BibleReferenceParser.Data;
using BibleReferenceParser.Parsing;
using NUnit.Framework;


namespace BibleReferenceParserTests
{
    public class ParserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ParseSingleBook()
        {
            var references = Parser.Parse("Matthew");
            Assert.IsNotNull(references);
            Assert.AreEqual(1, references.Count);
            {
                var referenceRange = references[0];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.IsNull(reference.Chapter);
                    Assert.IsNull(reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference);
                }
            }
        }

        [Test]
        public void ParseMultipleBooks()
        {
            var references = Parser.Parse("Matthew, Mark, Luke, John");
            Assert.IsNotNull(references);
            Assert.AreEqual(4, references.Count);
            {
                var referenceRange = references[0];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.IsNull(reference.Chapter);
                    Assert.IsNull(reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference);
                }
            }
            {
                var referenceRange = references[1];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Mark, reference.Book);
                    Assert.IsNull(reference.Chapter);
                    Assert.IsNull(reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference);
                }
            }
            {
                var referenceRange = references[2];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Luke, reference.Book);
                    Assert.IsNull(reference.Chapter);
                    Assert.IsNull(reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference);
                }
            }
            {
                var referenceRange = references[3];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.John, reference.Book);
                    Assert.IsNull(reference.Chapter);
                    Assert.IsNull(reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference);
                }
            }
        }

        [Test]
        public void ParseBookChapter()
        {
            var references = Parser.Parse("Matthew 3");
            Assert.IsNotNull(references);
            Assert.AreEqual(1, references.Count);
            {
                var referenceRange = references[0];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.IsNull(reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference);
                }
            }
        }

        [Test]
        public void ParseBookChapterRange()
        {
            var references = Parser.Parse("Matthew 3-19");
            Assert.IsNotNull(references);
            Assert.AreEqual(1, references.Count);
            {
                var referenceRange = references[0];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.IsNull(reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(19, reference.Chapter);
                    Assert.IsNull(reference.Verse);
                }
            }
        }

        [Test]
        public void ParseBookChapterVerse()
        {
            var references = Parser.Parse("Matthew 3:1");
            Assert.IsNotNull(references);
            Assert.AreEqual(1, references.Count);
            {
                var referenceRange = references[0];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(1, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference);
                }
            }
        }

        [Test]
        public void ParseBookChapterVerseRange()
        {
            var references = Parser.Parse("Matthew 3:1-2");
            Assert.IsNotNull(references);
            Assert.AreEqual(1, references.Count);
            {
                var referenceRange = references[0];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(1, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(2, reference.Verse);
                }
            }
        }

        [Test]
        public void ParseBookChapterVerseAndVerse()
        {
            var references = Parser.Parse("Matthew 3:1, 4");
            Assert.IsNotNull(references);
            Assert.AreEqual(2, references.Count);
            {
                var referenceRange = references[0];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(1, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference);
                }
            }
            {
                var referenceRange = references[1];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(4, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference); ;
                }
            }
        }

        [Test]
        public void ParseBookChapterVerseRangeAndVerse()
        {
            var references = Parser.Parse("Matthew 3:1-2, 4");
            Assert.IsNotNull(references);
            Assert.AreEqual(2, references.Count);
            {
                var referenceRange = references[0];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(1, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(2, reference.Verse);
                }
            }
            {
                var referenceRange = references[1];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(4, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference); ;
                }
            }
        }

        [Test]
        public void ParseBookChapterVerseAndVerseRange()
        {
            var references = Parser.Parse("Matthew 3:1, 3-4");
            Assert.IsNotNull(references);
            Assert.AreEqual(2, references.Count);
            {
                var referenceRange = references[0];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(1, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference);
                }
            }
            {
                var referenceRange = references[1];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(3, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(4, reference.Verse);
                }
            }
        }

        [Test]
        public void ParseBookChapterVerseAndVerseRangeAndChapterVerse()
        {
            var references = Parser.Parse("Matthew 3:1, 3-4, 4:1");
            Assert.IsNotNull(references);
            Assert.AreEqual(3, references.Count);
            {
                var referenceRange = references[0];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(1, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference);
                }
            }
            {
                var referenceRange = references[1];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(3, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(4, reference.Verse);
                }
            }
            {
                var referenceRange = references[2];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(4, reference.Chapter);
                    Assert.AreEqual(1, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference);
                }
            }
        }

        [Test]
        public void ParseBookRange()
        {
            var references = Parser.Parse("Psalms-Song of Solomon");
            Assert.IsNotNull(references);
            Assert.AreEqual(1, references.Count);
            {
                var referenceRange = references[0];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Psalms, reference.Book);
                    Assert.IsNull(reference.Chapter);
                    Assert.IsNull(reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Song_Of_Songs, reference.Book);
                    Assert.IsNull(reference.Chapter);
                    Assert.IsNull(reference.Verse);
                }
            }
        }

        [Test]
        public void ParseBookChapterVerseAndVerseAndBookChapterVerseRange()
        {
            var references = Parser.Parse("Matthew 3:1, 4, John 6:8-9");
            Assert.IsNotNull(references);
            Assert.AreEqual(3, references.Count);
            {
                var referenceRange = references[0];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(1, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference);
                }
            }
            {
                var referenceRange = references[1];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.Matthew, reference.Book);
                    Assert.AreEqual(3, reference.Chapter);
                    Assert.AreEqual(4, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNull(reference);
                }
            }
            {
                var referenceRange = references[2];
                {
                    var reference = referenceRange.First;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.John, reference.Book);
                    Assert.AreEqual(6, reference.Chapter);
                    Assert.AreEqual(8, reference.Verse);
                }
                {
                    var reference = referenceRange.Last;
                    Assert.IsNotNull(reference);
                    Assert.AreEqual(BibleBook.John, reference.Book);
                    Assert.AreEqual(6, reference.Chapter);
                    Assert.AreEqual(9, reference.Verse);
                }
            }
        }

        [Test]
        public void ParseInvalidBook()
        {
            Assert.Throws<InvalidOperationException>(() => Parser.Parse("FakeBook"));
        }

        [Test]
        public void ParseValidBookAndInvalidBook()
        {
            Assert.Throws<InvalidOperationException>(() => Parser.Parse("Matthew, FakeBook"));
        }

        [Test]
        public void ParseValidBookChapterAndInvalidBookChapter()
        {
            Assert.Throws<InvalidOperationException>(() => Parser.Parse("Matthew 3, FakeBook 3"));
        }
    }
}