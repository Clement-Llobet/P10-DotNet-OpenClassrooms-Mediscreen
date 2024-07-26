namespace Mediscreen.Domain.Note.Dto;

public record NotesCreateInput
{
    public required int PatientId { get; set; }
    public string? Note { get; set; }
    public required DateTime CreatedDate { get; set; }
    public string? Practitioner { get; set; } = string.Empty;
}
