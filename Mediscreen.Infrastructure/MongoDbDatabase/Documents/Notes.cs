using MongoDB.Bson.Serialization.Attributes;

namespace Mediscreen.Infrastructure.MongoDbDatabase.Documents;

public class Notes
{
    [BsonId]
    [BsonElement("patientId")]
    public int PatientId { get; set; }
    [BsonElement("note")]
    public string Note { get; set; } = string.Empty;
    [BsonElement("doctorId")]
    public int DoctorId { get; set; }
}
