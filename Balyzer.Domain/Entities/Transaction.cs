using Balyzer.Domain.ValueObjects;

namespace Balyzer.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }
    public DateOnly PurchaseDate { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public Category Category { get; private set; }
    public Card Card { get; private set; }
    public Installment Installment { get; private set; }
}