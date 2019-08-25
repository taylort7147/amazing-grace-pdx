using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageManager.Models
{
    public class Notes
    {
        public int Id { get; set; }

        [Display(Name = "URL")]
        [DataType(DataType.Url)]
        [Required]
        public string Url { get; set; }

        public int MessageId { get; set; }

        [ForeignKey(nameof(MessageId))]
        public Message Message { get; set; }
    }
}