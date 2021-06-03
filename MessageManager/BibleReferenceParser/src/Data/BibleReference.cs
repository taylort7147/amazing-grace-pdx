using System;
namespace BibleReferenceParser.Data
{
    public class BibleReference : IComparable, ICloneable
    {
        public object Clone()
        {
            var clone = new BibleReference
            {
                Book = Book,
                Chapter = Chapter,
                Verse = Verse
            };
            return clone;
        }

        public BibleBook Book { get; set; }

        public int? Chapter { get; set; }

        public int? Verse { get; set; }

        public string ToFriendlyString()
        {
            var str = Book.ToFriendlyString(hasChapter: (Chapter != null));
            if (Chapter != null)
            {
                str += $" {Chapter.Value}";
                if (Verse != null)
                {
                    str += $":{Verse}";
                }
            }
            return str;
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return 1;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return 0;
            }

            var otherReference = obj as BibleReference;
            if (otherReference is null)
            {
                return 1;
            }

            return GetHashCode().CompareTo(otherReference.GetHashCode());
        }

        public int GetId()
        {
            int bookPart = (int)Book;
            int chapterPart = Chapter.GetValueOrDefault(0);
            int versePart = Verse.GetValueOrDefault(0);
            int hashCode = (bookPart * 1000000) + (chapterPart * 1000) + (versePart);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return CompareTo(obj) == 0;
        }

        public override int GetHashCode()
        {
            return GetId();
        }
    }
}