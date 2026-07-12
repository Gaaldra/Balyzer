using Balyzer.Domain.Entities;
using Balyzer.Domain.ValueObjects;

namespace Balyzer.Tests.Domain.Builders;

public class TransactionBuilder
{
    public const string DefaultDescription = "RM Posto Central";
    public const decimal DefaultAmount = 54.52m;
    public static readonly DateOnly DefaultPurchaseDate = new(2025, 5, 14);
    private decimal _amount = DefaultAmount;
    private Card _card = new CardBuilder().Build();
    private Category _category = new CategoryBuilder().Build();
    private string _description = DefaultDescription;
    private Installment _installment = new(1, 1);

    private DateOnly _purchaseDate = DefaultPurchaseDate;

    public Transaction Build()
    {
        return new Transaction(_purchaseDate, _description, _amount, _category, _card, _installment);
    }

    public TransactionBuilder WithPurchaseDate(DateOnly purchaseDate)
    {
        _purchaseDate = purchaseDate;
        return this;
    }

    public TransactionBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public TransactionBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public TransactionBuilder WithCategory(Category category)
    {
        _category = category;
        return this;
    }

    public TransactionBuilder WithCard(Card card)
    {
        _card = card;
        return this;
    }

    public TransactionBuilder WithInstallment(Installment installment)
    {
        _installment = installment;
        return this;
    }
}