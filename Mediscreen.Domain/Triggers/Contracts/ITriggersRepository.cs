
namespace Mediscreen.Domain.Triggers.Contracts;

public interface ITriggersRepository : IQueryable<ITriggers>
{
    Task<IEnumerable<ITriggers>> GetAllTriggersAsync();
    Task<ITriggers> GetTriggerAsync(int triggerId);
}
