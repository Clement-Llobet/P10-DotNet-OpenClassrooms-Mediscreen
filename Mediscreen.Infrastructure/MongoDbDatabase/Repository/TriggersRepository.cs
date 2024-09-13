using Mediscreen.Domain.Triggers.Contracts;
using Mediscreen.Infrastructure.MongoDbDatabase.Documents;
using MongoDB.Driver;

namespace Mediscreen.Infrastructure.MongoDbDatabase.Repository;

public class TriggersRepository : QueryableRepositoryBase<ITriggers>, ITriggersRepository
{
    private readonly IMongoCollection<Triggers> _triggers;

    public TriggersRepository(IQueryable<ITriggers> triggers, MongoClient client)
        : base(triggers)
    {
        var database = client.GetDatabase("Mediscreen");
        _triggers = database.GetCollection<Triggers>("Triggers");
    }

    public async Task<IEnumerable<ITriggers>> GetAllTriggersAsync()
    {
        return await _triggers.Find(trigger => true).ToListAsync();
    }

    public async Task<ITriggers> GetTriggerAsync(int triggerId)
    {
        return await _triggers.Find(trigger => trigger.TriggerId == triggerId).FirstOrDefaultAsync();
    }
}
