using Mediscreen.Domain.Triggers.Contracts;
using Mediscreen.Domain.Triggers.Dto;

namespace Mediscreen.Domain.Triggers;

public class TriggersManager
{
    public static async Task<IEnumerable<TriggerDto>> ListTriggersAsync(ITriggersRepository triggersRepository)
    {
        var triggers = await triggersRepository.GetAllTriggersAsync();

        return triggers.Select(TriggerDto.Render);
    }
}
