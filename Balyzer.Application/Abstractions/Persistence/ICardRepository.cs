using Balyzer.Domain.Entities;

namespace Balyzer.Application.Abstractions.Persistence;

public interface ICardRepository
{
    Task<Card?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Card card, CancellationToken cancellationToken = default);
}