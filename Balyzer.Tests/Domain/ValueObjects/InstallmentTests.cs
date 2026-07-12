using Balyzer.Domain.Exceptions;
using Balyzer.Tests.Builders;

namespace Balyzer.Tests.Domain.ValueObjects;

public class InstallmentTests
{
    // Happy path
    [Fact]
    public void Should_CreateInstallment_When_DataIsValid()
    {
        var builder = new InstallmentBuilder();

        var installment = builder.Build();

        Assert.Equal(InstallmentBuilder.DefaultCurrent, installment.Current);
        Assert.Equal(InstallmentBuilder.DefaultTotal, installment.Total);
    }

    [Fact]
    public void Should_ReturnTrueForIsSingle_When_TotalIsOne()
    {
        var builder = new InstallmentBuilder();

        var installment = builder.Build();

        Assert.True(installment.IsSingle);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(12)]
    public void Should_ReturnFalseForIsSingle_When_TotalIsGreaterThanOne(int total)
    {
        var builder = new InstallmentBuilder().WithTotal(total);

        var installment = builder.Build();

        Assert.False(installment.IsSingle);
    }

    [Fact]
    public void Should_ReturnTrueForIsLast_When_CurrentEqualsTotal()
    {
        var builder = new InstallmentBuilder()
            .WithCurrent(5)
            .WithTotal(5);

        var installment = builder.Build();

        Assert.True(installment.IsLast);
    }

    [Theory]
    [InlineData(1, 5)]
    [InlineData(4, 5)]
    public void Should_ReturnFalseForIsLast_When_CurrentIsLessThanTotal(int current, int total)
    {
        var builder = new InstallmentBuilder()
            .WithCurrent(current)
            .WithTotal(total);

        var installment = builder.Build();

        Assert.False(installment.IsLast);
    }

    [Theory]
    [InlineData(1, 10, 9)]
    [InlineData(5, 5, 0)]
    [InlineData(3, 4, 1)]
    public void Should_CalculateRemainingInstallments_Correctly(int current, int total, int remaining)
    {
        var builder = new InstallmentBuilder()
            .WithCurrent(current)
            .WithTotal(total);

        var installment = builder.Build();

        Assert.Equal(remaining, installment.Remaining);
    }

    [Fact]
    public void Should_ReturnUnicaForToString_When_InstallmentIsSingle()
    {
        var builder = new InstallmentBuilder()
            .WithCurrent(1)
            .WithTotal(1);

        var installment = builder.Build();

        Assert.Equal("Única", installment.ToString());
    }

    [Theory]
    [InlineData(1, 5, "1/5")]
    [InlineData(2, 12, "2/12")]
    public void Should_ReturnFormattedStringForToString_When_InstallmentIsNotSingle(int current, int total,
        string expectedResult)
    {
        var builder = new InstallmentBuilder()
            .WithCurrent(current)
            .WithTotal(total);

        var installment = builder.Build();

        Assert.Equal(expectedResult, installment.ToString());
    }

    // Validation
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_ThrowException_When_TotalInstallmentIsLessThanOrEqualToZero(int invalidTotal)
    {
        var builder = new InstallmentBuilder()
            .WithTotal(invalidTotal);

        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal("O total de parcelas deve ser maior que zero.", exception.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_ThrowDomainException_When_CurrentInstallmentIsLessThanOrEqualToZero(int invalidCurrent)
    {
        var builder = new InstallmentBuilder()
            .WithCurrent(invalidCurrent);

        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal("O número da parcela deve ser maior que zero.", exception.Message);
    }

    [Fact]
    public void Should_ThrowDomainException_When_CurrentInstallmentIsGreaterThanTotal()
    {
        var builder = new InstallmentBuilder()
            .WithTotal(3)
            .WithCurrent(4);

        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal("A parcela atual não pode ser maior que o total de parcelas.", exception.Message);
    }
}