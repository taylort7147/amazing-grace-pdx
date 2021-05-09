using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MessageManager.Models
{
    public class Message
    {
        public int Id { get; set; }

        [MinLength(3)]
        [StringLength(120)]
        [RegularExpression("^[A-Z0-9].*$")]
        [Required]
        public string Title { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        [StringLength(512)]
        public string BibleReferences { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        public int? VideoId { get; set; }
        public Video Video { get; set; }

        public int? AudioId { get; set; }
        public Audio Audio { get; set; }

        public int? NotesId { get; set; }
        public Notes Notes { get; set; }

        public int? SeriesId { get; set; }

        [JsonIgnore]
        public Series Series { get; set; }

        public override string ToString()
        {
            return $"Message(Id={Id}, " +
                   $"Title={Title}, " +
                   $"Description={Description}, " +
                   $"AudioId={AudioId}, " +
                   $"NotesId={NotesId}, " +
                   $"VideoId={VideoId}, " +
                   $"SeriesId={SeriesId})";
        }
    }
}