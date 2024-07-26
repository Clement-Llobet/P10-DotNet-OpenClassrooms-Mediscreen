using Mediscreen.Infrastructure.MongoDbDatabase.Documents;
using Mediscreen.Infrastructure.MongoDbDatabase.Repository;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Mediscreen.Infrastructure.MongoDbDatabase.Context;

public class MediscreenMongoDBContext
{
    private readonly IMongoDatabase _database;

    public MediscreenMongoDBContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
        _database = client.GetDatabase("Mediscreen");
    }

    public IMongoCollection<Notes> Notes => _database.GetCollection<Notes>("PatientsNotes");
}
