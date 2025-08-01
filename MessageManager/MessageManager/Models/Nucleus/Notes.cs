using Microsoft.EntityFrameworkCore;

namespace MessageManager.Models.Nucleus
{
    [Index(nameof(NotesId), IsUnique = true)]
    public class Notes
    {
        public int Id { get; set; }

        public int NotesId { get; set; }

        public string NucleusNotesId { get; set; }

        public override string ToString()
        {
            return $"Notes(Id={Id}, " +
                   $"NotesId={NotesId}, " +
                   $"NucleusNotesId={NucleusNotesId})";
        }
    }
}
