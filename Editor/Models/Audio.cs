using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Editor.Models
{
    public class Audio
    {
        public int Id { get; set; }

        [Display(Name = "Stream URL")]
        [DataType(DataType.Url)]
        [Required]
        public string StreamUrl { get; set; }

        [Display(Name = "Download URL")]
        [DataType(DataType.Url)]
        [Required]
        public string DownloadUrl { get; set; }

        public int MessageId { get; set; }

        [ForeignKey(nameof(MessageId))]
        public Message Message { get; set; }
    }
}