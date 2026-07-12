using Balyzer.Domain.Entities;

namespace Balyzer.Application.Abstractions.Persistence;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
    Task<IAsyncEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken = default);
}