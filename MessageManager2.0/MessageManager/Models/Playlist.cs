using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageManager.Models
{
    public class Playlist
    {
        public int Id { get; set; }

        [StringLength(64)]
        [Display(Name = "YouTube Playlist ID")]
        [RegularExpression("[a-zA-Z0-9_-]{34}")] // Current implementation, not guaranteed by spec
        public string YouTubePlaylistId { get; set; }

        public int SeriesId { get; set; }

        [ForeignKey(nameof(SeriesId))]
        public Series Series { get; set; }

        public override string ToString()
        {
            return $"YouTubePlaylistId={YouTubePlaylistId}";
        }
    }
}