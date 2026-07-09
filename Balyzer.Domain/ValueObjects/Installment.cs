using Balyzer.Domain.Exceptions;

namespace Balyzer.Domain.ValueObjects;

public record Installment
{
    public int Current { get; }
    public int Total { get; }
    
    public bool IsSingle => Total == 1;
    public bool IsLast => Current == Total;
    public int Remaining => Total - Current;

    public Installment(int current, int total)
    {
        if (current <= 0)
            throw new DomainException("O número da parcela deve ser maior que zero.");
        
        if(total <= 0)
            throw new DomainException("O total de parcelas deve ser maior que zero.");

        if (current > total)
            throw new DomainException("A parcela atual não pode ser maior que o total de parcelas.");

        Current = current;
        Total = total;
    }

    public override string ToString() => IsSingle ? "Única" : $"{Current}/{Total}";
};