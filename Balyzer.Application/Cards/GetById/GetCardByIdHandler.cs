using Balyzer.Application.Abstractions.Persistence;
using Balyzer.Application.Cards.Common;

namespace Balyzer.Application.Cards.GetById;

public sealed class GetCardByIdHandler(ICardRepository cardRepository)
{
    public async Task<CardResponse?> HandleAsync(
        GetCardByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        var card = await cardRepository.GetByIdAsync(query.Id, cancellationToken);

        return card is null ? null : new CardResponse(card.Id, card.HolderName, card.LastDigits);
    }
}