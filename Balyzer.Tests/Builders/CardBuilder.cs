using Balyzer.Domain.Entities;

namespace Balyzer.Tests.Builders;

public class CardBuilder
{
    public const string DefaultLastDigits = "1234";
    public const string DefaultHolderName = "Gabriel Quadra";

    private string _holderName = DefaultHolderName;
    private string _lastDigits = DefaultLastDigits;

    public Card Build()
    {
        return new Card(_holderName, _lastDigits);
    }

    public CardBuilder WithHolderName(string holderName)
    {
        _holderName = holderName;
        return this;
    }

    public CardBuilder WithLastDigits(string lastDigits)
    {
        _lastDigits = lastDigits;
        return this;
    }
}