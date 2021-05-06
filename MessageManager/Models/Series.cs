using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageManager.Models
{
    public class Series
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(4095)]
        public string Description { get; set; }

        public int? PlaylistId { get; set; }

        public Playlist Playlist { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public override string ToString()
        {
            return $"Series(Id={Id}, " +
                   $"Name={Name})";
        }
    }
}