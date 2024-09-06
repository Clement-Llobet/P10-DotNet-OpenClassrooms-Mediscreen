namespace Mediscreen.UI.Models;

public class NotesViewModel
{
    public int NoteId { get; set; }
    public required int PatientId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public List<TriggerViewModel> Triggers { get; set; } = [];
    public string RiskLevel { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public string Practitioner { get; set; } = string.Empty;

}
