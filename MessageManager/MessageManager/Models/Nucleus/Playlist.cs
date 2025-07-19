namespace MessageManager.Models.Nucleus
{
    public class Playlist
    {
        public int Id { get; set; }

        public int SeriesId { get; set; }

        public string NucleusPlaylistId { get; set; }

        public override string ToString()
        {
            return $"Playlist(Id={Id}, " +
                   $"SeriesId={SeriesId}, " +
                   $"NucleusPlaylistId={NucleusPlaylistId})";
        }
    }
}
