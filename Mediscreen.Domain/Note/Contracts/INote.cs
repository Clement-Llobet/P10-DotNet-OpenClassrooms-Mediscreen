namespace Mediscreen.Domain.Note.Contracts;

public interface INotes
{
    public int PatientId { get; set; }
    public string Note { get; set; }
    public int DoctorId { get; set; }
    public DateTime CreatedDate { get; set; }
}
