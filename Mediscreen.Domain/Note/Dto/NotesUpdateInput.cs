using Mediscreen.Domain.Triggers.Contracts;
using Mediscreen.Domain.Triggers.Dto;

namespace Mediscreen.Domain.Note.Dto;

public record NotesUpdateInput
{
    public int NoteId { get; set; }
    public required int PatientId { get; set; }
    public DateTime NoteDate { get; set; }
    public string? Comment { get; set; }
    public List<int> Triggers { get; set; } = new();
    public required DateTime CurrentDateTime { get; set; }
    public required string Practitioner { get; set; } 
}
