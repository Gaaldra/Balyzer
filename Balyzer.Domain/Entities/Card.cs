using Balyzer.Domain.Exceptions;

namespace Balyzer.Domain.Entities;

public class Card
{
    public const int MaxHolderNameLength = 100;
    public Guid Id { get; private set; }
    public string HolderName { get; private set; } = string.Empty;
    public string LastDigits { get; private set; } = string.Empty;

    protected Card()
    {
        
    }

    public Card(string holderName, string lastDigits)
    {
        Id = Guid.CreateVersion7();

        ChangeHolderName(holderName);
        ChangeLastDigits(lastDigits);
    }

    private void ChangeHolderName(string holderName)
    {
        if (string.IsNullOrWhiteSpace(holderName))
            throw new DomainException("O titular do cartão é obrigatório.");

        holderName = holderName.Trim();

        if (holderName.Length > MaxHolderNameLength)
            throw new DomainException($"O titular não pode possuir mais de {MaxHolderNameLength} caracteres.");

        HolderName = holderName;
    }

    private void ChangeLastDigits(string lastDigits)
    {
        if (string.IsNullOrWhiteSpace(lastDigits))
            throw new DomainException("Os últimos dígitos do cartão são obrigatórios.");

        lastDigits = lastDigits.Trim();
        
        if(lastDigits.Length != 4)
            throw new DomainException("O cartão deve possuir exatamente quatro dígitos.");

        if (!lastDigits.All(c => c is >= '0' and <= '9'))
            throw new DomainException("O cartão deve conter apenas números.");

        LastDigits = lastDigits;
    }
}