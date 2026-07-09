namespace Balyzer.Domain.ValueObjects;

public record Installment(int Current, int Total)
{
    public bool IsSingle => Total == 1;
};