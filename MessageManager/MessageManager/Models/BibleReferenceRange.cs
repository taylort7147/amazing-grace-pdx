using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BibleReferenceParser.Data;

namespace MessageManager.Models
{
    public class BibleReferenceRange
    {
        [Key]
        public int Id { get; set; }

        public int StartBook { get; set; }
        public int StartChapter { get; set; }
        public int StartVerse { get; set; }

        public int EndBook { get; set; }
        public int EndChapter { get; set; }
        public int EndVerse { get; set; }

        public int MessageId { get; set; }

        [ForeignKey(nameof(MessageId))]
        [JsonIgnore]
        public Message Message { get; set; }

        public static BibleReferenceRange From(BibleReferenceParser.Data.BibleReferenceRange referenceRange)
        {
            var startReference = referenceRange.First;
            var endReference = referenceRange.Last == null ? referenceRange.First : referenceRange.Last;

            var model = new BibleReferenceRange();
            model.StartBook = (int)startReference.Book;
            model.StartChapter = startReference.Chapter.GetValueOrDefault(1);
            model.StartVerse = startReference.Verse.GetValueOrDefault(1);

            model.EndBook = (int)endReference.Book;
            model.EndChapter = endReference.Chapter.GetValueOrDefault(BibleDetails.GetLastChapterForBook(endReference.Book));
            model.EndVerse = endReference.Verse.GetValueOrDefault(BibleDetails.GetLastVerseForBookChapter(endReference.Book, model.EndChapter));

            return model;
        }

        public BibleReferenceParser.Data.BibleReferenceRange Deserialize()
        {
            var startReference = new BibleReference { Book = (BibleBook)StartBook, Chapter = StartChapter, Verse = StartVerse };
            var endReference = new BibleReference { Book = (BibleBook)EndBook, Chapter = EndChapter, Verse = EndVerse };
            var referenceRange = new BibleReferenceParser.Data.BibleReferenceRange { First = startReference, Last = endReference };
            return referenceRange;
        }

        public string ToFriendlyString()
        {
            var referenceRange = Deserialize();
            return referenceRange.ToFriendlyString();
        }

        public override string ToString()
        {
            return $"Message(Id={Id}, " +
                   $"Range={this.ToFriendlyString()})";
        }
    }
}