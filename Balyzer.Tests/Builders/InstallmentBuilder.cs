using Balyzer.Domain.ValueObjects;

namespace Balyzer.Tests.Builders;

public class InstallmentBuilder
{
    public const int DefaultTotal = 1;
    public const int DefaultCurrent = 1;

    private int _total = DefaultTotal;
    private int _current = DefaultCurrent;

    public Installment Build()
    {
        return new Installment(_current, _total);
    }

    public InstallmentBuilder WithTotal(int total)
    {
        _total = total;
        return this;
    }

    public InstallmentBuilder WithCurrent(int current)
    {
        _current = current;
        return this;
    }
}