using Mediscreen.UI.Models;

namespace Mediscreen.UI.Controllers.Services.TriggersService;

public interface ITriggersService
{
    Task<IEnumerable<TriggerViewModel>> GetAllTriggers();
}
