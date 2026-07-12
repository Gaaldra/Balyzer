using Balyzer.Application.Cards.Create;

namespace Balyzer.Tests.Application.Builders;

public class CreateCardCommandBuilder
{
    private string _holderName = "Juliana Silva";
    private string _lastDigits = "0987";

    public CreateCardCommandBuilder WithHolderName(string holderName)
    {
        _holderName = holderName;
        return this;
    }

    public CreateCardCommandBuilder WithLastDigits(string lastDigits)
    {
        _lastDigits = lastDigits;
        return this;
    }

    public CreateCardCommand Build()
    {
        return new CreateCardCommand(_holderName, _lastDigits);
    }
}