using Balyzer.Domain.Entities;
using Balyzer.Domain.Exceptions;
using Balyzer.Tests.Builders;

namespace Balyzer.Tests.Domain.Entities;

public class TransactionTests
{
    // Happy path
    [Fact]
    public void Should_Create_Transaction()
    {
        var builder = new TransactionBuilder();

        var transaction = builder.Build();

        Assert.NotEqual(Guid.Empty, transaction.Id);
        Assert.Equal(TransactionBuilder.DefaultDescription, transaction.Description);
        Assert.Equal(TransactionBuilder.DefaultAmount, transaction.Amount);
    }

    [Fact]
    public void Should_Create_Transaction_With_Max_Description_Length()
    {
        var description = new string('A', Transaction.DescriptionMaxLength);
        var builder = new TransactionBuilder().WithDescription(description);

        var transaction = builder.Build();

        Assert.Equal(description, transaction.Description);
    }

    [Fact]
    public void Should_Create_Transaction_With_Trim_Description()
    {
        var builder = new TransactionBuilder().WithDescription("   RM Posto Central   ");

        var transaction = builder.Build();

        Assert.Equal(TransactionBuilder.DefaultDescription, transaction.Description);
    }

    [Fact]
    public void Should_Create_Transaction_With_Minimum_Valid_Amount()
    {
        var amount = 0.01m;
        var builder = new TransactionBuilder().WithAmount(0.01m);

        var transaction = builder.Build();

        Assert.Equal(amount, transaction.Amount);
    }

    [Fact]
    public void Should_Create_Transaction_With_Round_Amount_To_Two_Decimals()
    {
        var builder = new TransactionBuilder().WithAmount(54.525m);
        
        var transaction = builder.Build();

        Assert.Equal(54.53m, transaction.Amount);
    }

    [Fact]
    public void Should_Create_Transaction_With_Today_Date()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var builder = new TransactionBuilder().WithPurchaseDate(today);

        var transaction = builder.Build();

        Assert.Equal(today, transaction.PurchaseDate);
    }

    // Validation

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void Should_Throw_When_Description_Is_Invalid(string invalidDescription)
    {
        var builder = new TransactionBuilder().WithDescription(invalidDescription);
        
        var action = builder.Build;

        var exception =
            Assert.Throws<DomainException>(action);

        Assert.Equal("A descrição é obrigatória!", exception.Message);
    }

    [Fact]
    public void Should_Throw_When_Description_Exceeds_Max_Length()
    {
        var description = new string('A', Transaction.DescriptionMaxLength + 1);
        var builder = new TransactionBuilder().WithDescription(description);

        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal($"A descrição não pode ser maior que {Transaction.DescriptionMaxLength} caracteres!",
            exception.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Should_Throw_When_Amount_Is_Zero_Or_Negative(decimal invalidAmount)
    {
        var builder = new TransactionBuilder().WithAmount(invalidAmount);
        
        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal("O valor deve ser maior que zero", exception.Message);
    }

    [Fact]
    public void Should_Throw_When_Purchase_Date_Is_Default()
    {
        var builder = new TransactionBuilder().WithPurchaseDate(default);
        
        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal("A data da compra é obrigatória!", exception.Message);
    }

    [Fact]
    public void Should_Throw_When_Purchase_Date_Is_In_The_Future()
    {
        var futurePurchaseDate = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
        var builder = new TransactionBuilder().WithPurchaseDate(futurePurchaseDate);

        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal("A data da compra não pode ser uma data futura!", exception.Message);
    }

    [Fact]
    public void Should_Throw_When_Category_Is_Null()
    {
        var builder = new TransactionBuilder().WithCategory(null!);
        
        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal("A categoria é obrigatória!", exception.Message);
    }

    [Fact]
    public void Should_Throw_When_Card_Is_Null()
    {
        var builder = new TransactionBuilder().WithCard(null!);
        
        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal("O cartão é obrigatório!", exception.Message);
    }

    [Fact]
    public void Should_Throw_When_Installment_Is_Null()
    {
        var builder = new TransactionBuilder().WithInstallment(null!);
        
        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal("A parcela é obrigatória!", exception.Message);
    }
}