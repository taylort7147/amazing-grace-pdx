using System;
using System.Text.Json;
using BibleReferenceValidator;
using NUnit.Framework;
using System.Collections.Generic;

namespace BibleReferenceValidatorTests
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
            var description = new BookDescription{
                Name="Genesis", 
                VerseCountsByChapter=new Dictionary<int, int>{{1, 31}, {2,  25}}};            
            var json = JsonSerializer.Serialize(description);
            Assert.AreEqual("{\"name\":\"Genesis\",\"verse_count_by_chapter\":{\"1\":31,\"2\":25}}", json);
        }

        [Test]
        public void CanDeserializeBookDescription()
        {
            var json = "{\"name\":\"Genesis\",\"verse_count_by_chapter\":{\"1\":31,\"2\":25}}";
            var description = JsonSerializer.Deserialize<BookDescription>(json);
            Assert.AreEqual("Genesis", description.Name);
            Assert.AreEqual(new Dictionary<int, int>{{1, 31}, {2, 25}}, description.VerseCountsByChapter);
        }
    }
}