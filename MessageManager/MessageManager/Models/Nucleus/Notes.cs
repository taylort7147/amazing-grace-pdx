namespace MessageManager.Models.Nucleus
{
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
