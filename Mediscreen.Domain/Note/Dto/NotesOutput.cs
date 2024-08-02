using Mediscreen.Domain.Note.Contracts;

namespace Mediscreen.Domain.Note.Dto;

public record NotesOutput
{
    public int? NoteId { get; set; } = 0;
    public required int PatientId { get; set; }
    public string Note { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string Practitioner { get; set; } = string.Empty;

    public static NotesOutput Render(INotes notes)
    {
        return new NotesOutput
        {
            NoteId = notes.NoteId,
            PatientId = notes.PatientId,
            Note = notes.Note,
            CreatedDate = notes.CreatedDate,
            Practitioner = notes.DoctorId
        };
    }
}
