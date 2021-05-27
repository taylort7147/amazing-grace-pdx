using System;
using System.Linq;

namespace BibleReferenceParser.Data
{
    public class BibleReferenceRange : IComparable
    {
        public BibleReference First { get; set; }

        public BibleReference Last { get; set; }

        public string ToFriendlyString()
        {
            // TODO: Better stringification
            var str = First.ToFriendlyString();
            if (Last != null)
            {
                str += "-" + Last.ToFriendlyString();
            }
            return str;
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