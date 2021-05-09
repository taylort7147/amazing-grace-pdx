using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Message Message { get; set; }

        public override string ToString()
        {
            return $"Notes(Id={Id}, " +
                   $"Url={Url}, " +
                   $"MessageId={MessageId})";
        }
    }
}