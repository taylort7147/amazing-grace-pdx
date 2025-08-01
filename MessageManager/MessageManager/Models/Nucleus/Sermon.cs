using Microsoft.EntityFrameworkCore;

namespace MessageManager.Models.Nucleus
{
    [Index(nameof(MessageId), IsUnique = true)]
    public class Sermon
    {
        public int Id { get; set; }

        public int MessageId { get; set; }

        public string NucleusChurchId { get; set; }

        public string NucleusSermonEngineId { get; set; }

        public string NucleusSermonId { get; set; }

        public override string ToString()
        {
            return $"Sermon(Id={Id}, " +
                   $"MessageId={MessageId}, " +
                   $"NucleusSermonId={NucleusSermonId})";
        }
    }
}
