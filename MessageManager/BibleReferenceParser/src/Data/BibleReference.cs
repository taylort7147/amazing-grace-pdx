using System;
namespace BibleReferenceParser.Data
{
    public class BibleReference : IComparable
    {
        public BibleBook Book { get; set; }

        public int? Chapter { get; set; }

        public int? Verse { get; set; }

        public string ToFriendlyString()
        {
            var str = Book.ToFriendlyString();
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

            return ((IComparable)new Tuple<BibleBook, int?, int?>(this.Book, this.Chapter, this.Verse))
                .CompareTo(new Tuple<BibleBook, int?, int?>(otherReference.Book, otherReference.Chapter, otherReference.Verse));

        }

        public override bool Equals(object obj)
        {
            return CompareTo(obj) == 0;
        }
    }
}