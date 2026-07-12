using Balyzer.Application.Abstractions.Persistence;
using Balyzer.Application.Cards.Create;
using Balyzer.Domain.Entities;
using Moq;

namespace Balyzer.Tests.Application.Cards.Create;

public class CreateCardHandlerTests
{
    private readonly Mock<ICardRepository> _cardRepositoryMock;
    private readonly CreateCardHandler _handler;

    public CreateCardHandlerTests()
    {
        _cardRepositoryMock = new Mock<ICardRepository>();
        _handler = new CreateCardHandler(_cardRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_HandlerAsyncReturnId_When_CommandIsValid()
    {
        var command = new CreateCardCommand("JOHN DOE", "1234");

        var result = await _handler.HandleAsync(command, CancellationToken.None);
        
        Assert.NotEqual(Guid.Empty, result);
        
        _cardRepositoryMock.Verify(repo => 
            repo.AddAsync(It.IsAny<Card>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}