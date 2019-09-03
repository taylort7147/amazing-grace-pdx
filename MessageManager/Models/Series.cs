using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace MessageManager.Models
{
public class Series {
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public IEnumerable<Message> Messages { get; set; }
}
}