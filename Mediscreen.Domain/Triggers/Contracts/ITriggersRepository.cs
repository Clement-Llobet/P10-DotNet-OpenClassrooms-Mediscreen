namespace Mediscreen.Domain.Triggers.Contracts;

public interface ITriggersRepository : IQueryable<ITriggers>
{
    Task<ITriggers> GetTriggerAsync(int triggerId);
}
