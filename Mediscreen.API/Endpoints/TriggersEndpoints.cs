using Mediscreen.API.Routes;
using Mediscreen.Domain.Triggers;
using Mediscreen.Domain.Triggers.Contracts;

namespace Mediscreen.API.Endpoints;

public static class TriggersEndpoints
{
    public static void MapTriggersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Triggers.ListTriggers, async (ITriggersRepository triggersRepository) =>
        {
            return await TriggersManager.ListTriggersAsync(triggersRepository);
        }).WithTags(ApiRoutes.Triggers.Tag)
          .WithMetadata(ApiRoutes.Triggers.ListTriggersMetadata);
    }
}
