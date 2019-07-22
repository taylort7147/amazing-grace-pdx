using System.ComponentModel.DataAnnotations;

namespace Editor.Models
{
    public class Notes
    {
        public int Id { get; set; }

        [Display(Name = "URL")]
        [DataType(DataType.Url)]
        [Required]
        public string Url { get; set; }

        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}