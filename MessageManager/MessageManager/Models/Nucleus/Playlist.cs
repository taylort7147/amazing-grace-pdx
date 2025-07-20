using Microsoft.EntityFrameworkCore;

namespace MessageManager.Models.Nucleus
{
    [Index(nameof(SeriesId), IsUnique = true)]
    public class Playlist
    {
        public int Id { get; set; }

        public int SeriesId { get; set; }

        public string NucleusChurchId { get; set; }

        public string NucleusSermonEngineId { get; set; }
        
        public string NucleusPlaylistId { get; set; }

        public override string ToString()
        {
            return $"Playlist(Id={Id}, " +
                   $"SeriesId={SeriesId}, " +
                   $"NucleusPlaylistId={NucleusPlaylistId})";
        }
    }
}
