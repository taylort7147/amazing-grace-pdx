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
        public int MessageStartTime { get; set; }

        // Derived from MessageStartTime
        // Convenience for calculations and string conversion
        [NotMapped]
        public TimeSpan MessageStartTimeSpan
        {
            get
            {
                return TimeSpan.FromSeconds(MessageStartTime);
            }
            set
            {
                MessageStartTime = (int) value.TotalSeconds;
            }
        }

        // Derived from MessageStartTimeSpan
        // Used for textual representation
        [NotMapped]
        [RegularExpression("^((((([0-9]{0,2}:)?[0-5])?[0-9]:)?[0-5])?[0-9])?$")]
        public string MessageStartTimeString
        {
            get {
                return MessageStartTimeSpan.ToString(@"hh\:mm\:ss");
            }
            set
            {
                if(value == null || value.Length == 0)
                {
                    MessageStartTimeSpan = new TimeSpan(0);
                }
                else
                {
                    var x = TimeSpan.ParseExact(value, new string[]
                    {
                        "%s", "ss", @"m\:ss", @"mm\:ss", @"h\:mm\:ss", @"hh\:mm\:ss"
                    }, null);
                    MessageStartTimeSpan = x;
                }
            }
        }

        public int MessageId { get; set; }

        [ForeignKey(nameof(MessageId))]
        public Message Message { get; set; }
    }
}