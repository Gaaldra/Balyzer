using Balyzer.Application.Abstractions.Persistence;
using Balyzer.Domain.Entities;

namespace Balyzer.Application.Cards.Create;

public sealed class CreateCardHandler(ICardRepository cardRepository)
{
    public async Task<Guid> HandleAsync(
        CreateCardCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var card = new Card(command.HolderName, command.LastDigits);

        await cardRepository.AddAsync(card, cancellationToken);
        
        return card.Id;
    }
}