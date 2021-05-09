using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MessageManager.Models
{
    public class Video
    {
        public int Id { get; set; }


        [Display(Name = "Video ID")]
        [RegularExpression("[a-zA-Z0-9_-]{11}")] // Current implementation, not guaranteed by spec
        [Required]
        public string YouTubeVideoId { get; set; }

        public int MessageStartTimeSeconds { get; set; }

        // Derived from MessageStartTime
        // Convenience for calculations and string conversion
        [NotMapped]
        public TimeSpan MessageStartTimeSpan
        {
            get
            {
                return TimeSpan.FromSeconds(MessageStartTimeSeconds);
            }
            set
            {
                MessageStartTimeSeconds = (int)value.TotalSeconds;
            }
        }

        // Derived from MessageStartTimeSpan
        // Used for textual representation
        [NotMapped]
        [Display(Name = "Message Start Time")]
        [RegularExpression("^((((([0-9]{0,2}:)?[0-5])?[0-9]:)?[0-5])?[0-9])?$")]
        public string MessageStartTimeString
        {
            get
            {
                return MessageStartTimeSpan.ToString(@"hh\:mm\:ss");
            }
            set
            {
                if (value == null || value.Length == 0)
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
        [JsonIgnore]
        public Message Message { get; set; }

        public override string ToString()
        {
            return $"Video(Id={Id}, " +
                   $"YouTubeVideoId={YouTubeVideoId}, " +
                   $"MessageStartTimeSeconds={MessageStartTimeSeconds}, " +
                   $"MessageId={MessageId})";
        }
    }
}