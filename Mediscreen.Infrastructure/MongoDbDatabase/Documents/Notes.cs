using Mediscreen.Domain.Note.Contracts;
using MongoDB.Bson.Serialization.Attributes;

namespace Mediscreen.Infrastructure.MongoDbDatabase.Documents;

public class Notes : INotes
{
    [BsonId]
    [BsonElement("patientId")]
    public int PatientId { get; set; }
    [BsonElement("note")]
    public string Note { get; set; } = string.Empty;
    [BsonElement("doctorId")]
    public int DoctorId { get; set; }
    [BsonElement("createdDate")]
    public DateTime CreatedDate { get; set; }
    [BsonElement("updatedDate")]
    public DateTime LastUpdatdDate { get; set; }
}
