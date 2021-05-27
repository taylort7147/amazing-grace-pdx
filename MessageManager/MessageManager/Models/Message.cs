using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using BibleReferenceParser.Parsing;
using MessageManager.Utility;

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

        [Display(Name = "Bible References")]
        public List<BibleReferenceRange> BibleReferences { get; set; }

        [NotMapped]
        [Display(Name = "Bible References")]
        [BibleReferenceValidation]
        public string BibleReferencesString
        {
            get
            {
                if (BibleReferences == null)
                {
                    return "";
                }
                var bibleReferenceStrings = BibleReferences.Select(x => x.ToFriendlyString());
                return string.Join(", ", bibleReferenceStrings);
            }
            set
            {
                var convertedList = new List<BibleReferenceRange>();
                if (value != null)
                {
                    var parsedList = Parser.Parse(value);
                    foreach (var referenceRange in parsedList)
                    {
                        var referenceRangeModel = BibleReferenceRange.From(referenceRange);
                        referenceRangeModel.MessageId = Id;
                        convertedList.Add(referenceRangeModel);
                    }
                }
                BibleReferences = convertedList;
            }
        }

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