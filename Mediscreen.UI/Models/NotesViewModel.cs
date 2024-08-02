namespace Mediscreen.UI.Models;

public class NotesViewModel
{
    public int NoteId { get; set; }
    public required int PatientId { get; set; }
    public string Note { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public int PractitionerId { get; set; }
    public string Practitioner { get; set; } = string.Empty;

}
