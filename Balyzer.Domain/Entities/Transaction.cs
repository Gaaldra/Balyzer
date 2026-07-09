using Balyzer.Domain.Exceptions;
using Balyzer.Domain.ValueObjects;

namespace Balyzer.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }
    public DateOnly PurchaseDate { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public Category Category { get; private set; } = null!;
    public Card Card { get; private set; } = null!;
    public Installment Installment { get; private set; } = null!;

    public const int DescriptionMaxLength = 150;

    protected Transaction()
    {
    }

    public Transaction(
        DateOnly purchaseDate,
        string description,
        decimal amount,
        Category category,
        Card card,
        Installment installment)
    {
        Id = Guid.CreateVersion7();

        ChangePurchaseDate(purchaseDate);
        ChangeDescription(description);
        ChangeAmount(amount);
        ChangeCategory(category);
        ChangeCard(card);

        ChangeInstallment(installment);
    }

    private void ChangePurchaseDate(DateOnly purchaseDate)
    {
        if (purchaseDate == default)
            throw new DomainException("A data da compra é obrigatória!");

        if (purchaseDate > DateOnly.FromDateTime(DateTime.Today))
            throw new DomainException("A data da compra não pode ser uma data futura!");

        PurchaseDate = purchaseDate;
    }

    private void ChangeDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("A descrição é obrigatória!");

        description = description.Trim();

        if (description.Length > DescriptionMaxLength)
            throw new DomainException($"A descrição não pode ser maior que {DescriptionMaxLength} caracteres!");
        
        Description = description;
    }

    private void ChangeAmount(decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("O valor deve ser maior que zero");

        Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
    }

    private void ChangeCategory(Category category)
    {
        Category = category ?? throw new DomainException("A categoria é obrigatória!");
    }

    private void ChangeCard(Card card)
    {
        Card = card ?? throw new DomainException("O cartão é obrigatório!");
    }

    private void ChangeInstallment(Installment installment)
    {
        Installment = installment ?? throw new DomainException("A parcela é obrigatória!");
    }
}