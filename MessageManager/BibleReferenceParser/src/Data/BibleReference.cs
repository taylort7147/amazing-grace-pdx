namespace BibleReferenceParser.Data
{
    public class BibleReference
    {
        public BibleBook Book { get; set; }

        public int? Chapter { get; set; }

        public int? Verse { get; set; }
    }
}