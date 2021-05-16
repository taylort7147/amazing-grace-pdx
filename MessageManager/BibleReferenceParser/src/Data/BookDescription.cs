using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace BibleReferenceParser.Data
{
    public class BookDescription
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public Dictionary<int, int> VerseCountsByChapter { get; set; }

        [JsonInclude]
        public string name { get { return Name; } set { Name = value; } }

        [JsonInclude]
        public Dictionary<string, int> verse_count_by_chapter
        {
            get
            {
                return VerseCountsByChapter.ToDictionary(x => x.Key.ToString(), x => x.Value);
            }
            set
            {
                VerseCountsByChapter = value.ToDictionary(x => int.Parse(x.Key), x => x.Value);
            }
        }
    }
}