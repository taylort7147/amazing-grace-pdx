namespace MessageManager.Models.Nucleus
{
    public class Sermon
    {
        public int Id { get; set; }

        public int MessageId { get; set; }

        public string NucleusSermonId { get; set; }

        public override string ToString()
        {
            return $"Sermon(Id={Id}, " +
                   $"MessageId={MessageId}, " +
                   $"NucleusSermonId={NucleusSermonId})";
        }
    }
}
