using System;

namespace BibleReferenceParser.Data
{
    public class BibleReferenceBuilder
    {
        private string Book;
        private int? Chapter;
        private int? Verse;

        public BibleReferenceBuilder SetBook(string book)
        {
            if (Book != null)
            {
                Chapter = null;
                Verse = null;
            }
            Book = book;
            return this;
        }

        public BibleReferenceBuilder SetChapter(int chapter)
        {
            if (Book == null)
            {
                throw new InvalidOperationException("Must set book before setting chapter");
            }
            if (Chapter != null)
            {
                Verse = null;
            }
            Chapter = chapter;
            return this;
        }

        public BibleReferenceBuilder SetVerse(int verse)
        {
            if (Chapter == null)
            {
                throw new InvalidOperationException("Must set chapter before setting verse");
            }
            Verse = verse;
            return this;
        }

        public BibleReference Build()
        {
            if (Book == null)
            {
                throw new InvalidOperationException("Book required");
            }

            var reference = new BibleReference { Book = Book, Chapter = Chapter, Verse = Verse };
            return reference;
        }

    }
}