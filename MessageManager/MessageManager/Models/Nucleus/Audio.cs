using Microsoft.EntityFrameworkCore;

namespace MessageManager.Models.Nucleus
{
    [Index(nameof(AudioId), IsUnique = true)]
    public class Audio
    {
        public int Id { get; set; }

        public int AudioId { get; set; }

        public string NucleusAudioId { get; set; }

        public override string ToString()
        {
            return $"Audio(Id={Id}, " +
                   $"AudioId={AudioId}, " +
                   $"NucleusAudioId={NucleusAudioId})";
        }
    }
}
