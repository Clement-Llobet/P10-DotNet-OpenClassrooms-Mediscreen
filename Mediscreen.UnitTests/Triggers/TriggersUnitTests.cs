using Mediscreen.Domain.Triggers;
using Mediscreen.Domain.Triggers.Contracts;
using Mediscreen.Domain.Triggers.Dto;
using Mediscreen.Infrastructure.MongoDbDatabase.Documents;
using Moq;

namespace Mediscreen.UnitTests.Triggers;

public class TriggersUnitTests
{
    [Fact]
    public async Task ListTriggersAsync_ShouldReturnMappedTriggerDtos()
    {
        // Arrange
        var mockRepository = new Mock<ITriggersRepository>();

        Mock<ITriggers> trigger1 = new();
        trigger1.SetupGet(t => t.TriggerId).Returns(1);
        trigger1.SetupGet(t => t.TriggerName).Returns("Trigger1");

        Mock<ITriggers> trigger2 = new();
        trigger2.SetupGet(t => t.TriggerId).Returns(2);
        trigger2.SetupGet(t => t.TriggerName).Returns("Trigger2");

        Mock<ITriggers> trigger3 = new();
        trigger3.SetupGet(t => t.TriggerId).Returns(3);
        trigger3.SetupGet(t => t.TriggerName).Returns("Trigger3");

        var triggers = new List<ITriggers> { trigger1.Object, trigger2.Object, trigger3.Object };

        mockRepository.Setup(repo => repo.GetAllTriggersAsync())
            .ReturnsAsync(triggers);

        // Act
        var result = await TriggersManager.ListTriggersAsync(mockRepository.Object);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<TriggerDto>>(result);

        var triggerDtos = result.ToList();
        Assert.Equal(3, triggerDtos.Count);

        for (int i = 0; i < triggers.Count; i++)
        {
            Assert.Equal(triggers[i].TriggerId, triggerDtos[i].TriggerId);
            Assert.Equal(triggers[i].TriggerName, triggerDtos[i].TriggerName);
        }

        mockRepository.Verify(repo => repo.GetAllTriggersAsync(), Times.Once);
    }
}
