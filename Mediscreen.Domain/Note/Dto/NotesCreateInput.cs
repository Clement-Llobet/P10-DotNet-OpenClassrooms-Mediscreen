using Mediscreen.Domain.Triggers.Contracts;
using Mediscreen.Domain.Triggers.Dto;

namespace Mediscreen.Domain.Note.Dto;

public record NotesCreateInput
{
    public required int PatientId { get; set; }
    public required int NoteId { get; set; }
    public required string Comment { get; set; } = string.Empty;
    public required List<int> Triggers { get; set; } = [];
    public required DateTime CreatedDate { get; set; }
    public string? Practitioner { get; set; } = string.Empty;
}
