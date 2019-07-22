using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Editor.Models
{
    public class Video
    {
        public int Id { get; set; }


        [Display(Name = "Video ID")]
        [RegularExpression("[a-zA-Z0-9_-]{11}")] // Current implementation, not guaranteed by spec
        [Required]
        public string YouTubeVideoId { get; set; }


        [Display(Name = "Playlist ID")]
        [RegularExpression("[a-zA-Z0-9_-]{18}")] // Current implementation, not guaranteed by spec
        public string YouTubePlaylistId { get; set; }

        [Display(Name = "Message Start Time")]
        [DataType(DataType.Time)]
        public DateTime MessageStartTime { get; set; }

        public int MessageId { get; set; }

        [ForeignKey(nameof(MessageId))]
        public Message Message { get; set; }
    }
}