using Mediscreen.Domain.Triggers.Contracts;

namespace Mediscreen.Domain.Triggers.Dto;

public record TriggerDto
{
    public required int TriggerId { get; set; }
    public required string TriggerName { get; set; } = string.Empty;
}
