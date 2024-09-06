using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Triggers.Contracts;
using MongoDB.Bson.Serialization.Attributes;

namespace Mediscreen.Infrastructure.MongoDbDatabase.Documents;

public class Notes : INotes
{
    [BsonId]
    [BsonElement("noteId")]
    public int NoteId { get; set; }
    [BsonElement("patientId")]
    public int PatientId { get; set; }
    [BsonElement("noteDate")]
    public DateTime NoteDate { get; set; }
    [BsonElement("note")]
    public string Comment { get; set; } = string.Empty;
    [BsonElement("triggersList")]
    public List<ITriggers> Triggers { get; set; } = [];
    [BsonElement("doctorId")]
    public string DoctorId { get; set; } = string.Empty;
    [BsonElement("createdDate")]
    public DateTime CreatedDate { get; set; }
    [BsonElement("updatedDate")]
    public DateTime LastUpdatedDate { get; set; }
    [BsonElement("riskLevel")]
    public string RiskLevel { get; set; } = string.Empty;
}
