using System;
using System.ComponentModel.DataAnnotations;

namespace Editor.Models
{
    public class Message
    {
        public int Id { get; set; }

        [MinLength(3)]
        [StringLength(120)]
        [RegularExpression("^[A-Z0-9].*$")]
        [Required]
        public string Title { get; set; }

        [StringLength(4095)]
        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        public int? VideoId { get; set; }
        public Video Video { get; set; }

        public int? AudioId { get; set; }
        public Audio Audio { get; set; }

        [Required]
        public int? NotesId { get; set; }
        public Notes Notes { get; set; }
    }
}