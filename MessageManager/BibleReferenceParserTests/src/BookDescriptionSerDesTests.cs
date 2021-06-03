using System;
using System.Collections.Generic;
using System.Text.Json;
using BibleReferenceParser.Data;
using NUnit.Framework;

namespace BibleReferenceParserTests
{
    public class BookDescriptionSerDesTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanSerializeBookDescription()
        {
            var description = new BookDescription
            {
                Book = BibleBook.Genesis,
                VerseCountsByChapter = new Dictionary<int, int> { { 1, 31 }, { 2, 25 } }
            };
            var json = JsonSerializer.Serialize(description);
            Assert.AreEqual("{\"book\":\"Genesis\",\"verse_count_by_chapter\":{\"1\":31,\"2\":25}}", json);
        }

        [Test]
        public void CanDeserializeBookDescription()
        {
            var json = "{\"book\":\"Genesis\",\"verse_count_by_chapter\":{\"1\":31,\"2\":25}}";
            var description = JsonSerializer.Deserialize<BookDescription>(json);
            Assert.AreEqual(BibleBook.Genesis, description.Book);
            Assert.AreEqual(new Dictionary<int, int> { { 1, 31 }, { 2, 25 } }, description.VerseCountsByChapter);
        }
    }
}