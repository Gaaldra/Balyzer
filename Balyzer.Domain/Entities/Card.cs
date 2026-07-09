namespace Balyzer.Domain.Entities;

public class Card
{
    public Guid Id { get; private set; }
    public string HolderName { get; private set; }
    public string LastDigits { get; private set; }
}