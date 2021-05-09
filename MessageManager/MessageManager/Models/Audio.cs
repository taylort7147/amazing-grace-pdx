using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MessageManager.Models
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
        [JsonIgnore]
        public Message Message { get; set; }

        // Private storage for Google Drive ID
        [NotMapped]
        private string _GoogleDriveId { get; set; }

        // Used to set StreamUrl and DownloadUrl for Google Drive links
        [NotMapped]
        [Display(Name = "Google Drive ID")]
        [RegularExpression("^[0-9a-zA-Z_\\-]+$")]
        public string GoogleDriveId
        {
            get
            {
                return _GoogleDriveId;
            }
            set
            {
                _GoogleDriveId = value;
                StreamUrl = "https://drive.google.com/open?id=" + value;
                DownloadUrl = "https://drive.google.com/uc?export=download&id=" + value;
            }
        }

        public override string ToString()
        {
            return $"Audio(Id={Id}, " +
                   $"StreamUrl={StreamUrl}, " +
                   $"DownloadUrl={DownloadUrl}, " +
                   $"MessageId={MessageId})";
        }
    }
}