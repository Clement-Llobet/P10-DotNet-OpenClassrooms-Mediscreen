using Mediscreen.Domain.Note.Contracts;

namespace Mediscreen.Domain.Note.Dto;

public record NotesOutput
{
    public int? NoteId { get; set; } = 0;
    public required int PatientId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public List<string> Triggers { get; set; } = new();
    public DateTime CreatedDate { get; set; }
    public string Practitioner { get; set; } = string.Empty;

    public static NotesOutput Render(INotes notes)
    {
        return new NotesOutput
        {
            NoteId = notes.NoteId,
            PatientId = notes.PatientId,
            Comment = notes.Comment,
            Triggers = notes.Triggers.Select(trigger => trigger.ToString()).ToList(),
            CreatedDate = notes.CreatedDate,
            Practitioner = notes.DoctorId
        };
    }
}
