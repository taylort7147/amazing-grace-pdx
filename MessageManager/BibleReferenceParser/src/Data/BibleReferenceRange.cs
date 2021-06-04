using System;
using System.Linq;

namespace BibleReferenceParser.Data
{
    public class BibleReferenceRange : IComparable, ICloneable
    {
        public object Clone()
        {
            var clone = new BibleReferenceRange { First = null, Last = null };
            if (First != null)
            {
                clone.First = First.Clone() as BibleReference;
            }
            if (Last != null)
            {
                clone.Last = Last.Clone() as BibleReference;
            }
            return clone;
        }

        public BibleReference First { get; set; }

        public BibleReference Last { get; set; }

        public BibleReferenceRange GetExplicitRange()
        {
            var range = this.Clone() as BibleReferenceRange;

            if (range.Last == null)
            {
                range.Last = new BibleReference { Book = range.First.Book, Chapter = range.First.Chapter, Verse = range.First.Verse };
            }

            var lastChapterOfBook = BibleDetails.GetLastChapterForBook(range.Last.Book);

            range.First.Chapter = range.First.Chapter.GetValueOrDefault(1);
            range.First.Verse = range.First.Verse.GetValueOrDefault(1);
            range.Last.Chapter = range.Last.Chapter.GetValueOrDefault(lastChapterOfBook);
            range.Last.Verse = range.Last.Verse.GetValueOrDefault(BibleDetails.GetLastVerseForBookChapter(range.Last.Book, range.Last.Chapter.Value));

            return range;
        }

        public string ToFriendlyString()
        {
            var range = GetExplicitRange();

            // Single reference
            if (range.First.Equals(range.Last))
            {
                // [Book]( [Chapter]( [Verse]))
                return range.First.ToFriendlyString();
            }

            // Reference range within book
            if (range.First.Book == range.Last.Book)
            {
                var lastChapter = BibleDetails.GetLastChapterForBook(range.Last.Book);
                var lastVerse = BibleDetails.GetLastVerseForBookChapter(range.Last.Book, range.Last.Chapter.Value);

                // Reference is a single book
                if (range.First.Chapter == 1 &&
                    range.First.Verse == 1 &&
                    range.Last.Chapter == lastChapter &&
                    range.Last.Verse == lastVerse)
                {
                    // [Book]
                    return range.First.Book.ToFriendlyString();
                }

                // Reference within chapter
                if (range.First.Chapter == range.Last.Chapter)
                {
                    // Reference is whole chapter
                    if (range.First.Verse == 1 && range.Last.Verse == BibleDetails.GetLastVerseForBookChapter(range.Last.Book, range.Last.Chapter.Value))
                    {
                        // [Boook] [Chapter]
                        return range.First.Book.ToFriendlyString() + " " + range.First.Chapter.ToString();
                    }

                    // [Book] [Chapter]:[Verse]-[Verse]
                    return range.First.ToFriendlyString() + "-" + range.Last.Verse.ToString();
                }

                // Reference is a range across chapters
                {
                    var isFirstChapterWhole = range.First.Verse == 1;
                    var isLastChapterWhole = range.Last.Verse == BibleDetails.GetLastVerseForBookChapter(range.Last.Book, range.Last.Chapter.Value);

                    if (isFirstChapterWhole)
                    {
                        if (isLastChapterWhole)
                        {
                            // [Book] [Chapter]-[Chapter]
                            return range.First.Book.ToFriendlyString() + " " + range.First.Chapter.ToString() + "-" + range.Last.Chapter.ToString();
                        }
                        else
                        {
                            // [Book] [Chapter]-[Chapter]:[Verse]
                            return range.First.Book.ToFriendlyString() + " " + range.First.Chapter.ToString() + "-" + range.Last.Chapter.ToString() + ":" + range.Last.Verse.ToString();
                        }
                    }
                    else
                    {
                        // [Book] [Chapter]:[Verse]-[Chapter]:[Verse]
                        return range.First.Book.ToFriendlyString() + " " + range.First.Chapter.ToString() + ":" + range.First.Verse.ToString() + "-" + range.Last.Chapter.ToString() + ":" + range.Last.Verse.ToString();
                    }
                }
            }

            // Reference range across books
            else
            {
                var str = First.ToFriendlyString();
                if (Last != null)
                {
                    str += "-" + Last.ToFriendlyString();
                }
                return str;
            }
        }

        public int CompareTo(object obj)
        {
            var other = obj as BibleReferenceRange;
            if (other == null)
            {
                return 1;
            }
            if (ReferenceEquals(this, other))
            {
                return 0;
            }
            return GetId().CompareTo(other.GetId());
        }

        public override bool Equals(object obj)
        {
            return CompareTo(obj) == 0;
        }

        public Int64 GetId()
        {
            Int64 id = 0;
            if (First != null)
            {
                id += ((Int64)First.GetId() << 32);
            }
            if (Last != null)
            {
                id += Last.GetId();
            }
            return id;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}