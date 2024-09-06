using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Triggers.Contracts;

namespace Mediscreen.Domain.Note.Dto;

public record NotesOutput
{
    public int? NoteId { get; set; } = 0;
    public required int PatientId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public List<ITriggers> Triggers { get; set; } = [];
    public DateTime LastUpdatedDate { get; set; }
    public string Practitioner { get; set; } = string.Empty;
    public string RiskLevel { get; set; } = string.Empty;

    public static NotesOutput Render(INotes notes)
    {
        return new NotesOutput
        {
            NoteId = notes.NoteId,
            PatientId = notes.PatientId,
            Comment = notes.Comment,
            Triggers = notes.Triggers,
            LastUpdatedDate = notes.LastUpdatedDate,
            Practitioner = notes.DoctorId,
            RiskLevel = notes.RiskLevel
        };
    }
}
