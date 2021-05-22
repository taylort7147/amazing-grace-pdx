using System.Linq;

namespace BibleReferenceParser.Data
{
    public class BibleReferenceRange
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
    }
}