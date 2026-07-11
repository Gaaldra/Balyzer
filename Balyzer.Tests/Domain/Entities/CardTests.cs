using Balyzer.Domain.Entities;
using Balyzer.Domain.Exceptions;
using Balyzer.Tests.Builders;

namespace Balyzer.Tests.Domain.Entities;

public class CardTests
{
    // Happy paths
    [Fact]
    public void Should_Create_Card_With_UUID_Version_7()
    {
        var builder = new CardBuilder();

        var card = builder.Build();
        var guidStr = card.Id.ToString("N");
        
        Assert.Equal('7', guidStr[12]);
        Assert.Equal(CardBuilder.DefaultHolderName, card.HolderName);
        Assert.Equal(CardBuilder.DefaultLastDigits, card.LastDigits);
    }

    [Fact]
    public void Should_Create_Card_With_Max_HolderName_Length()
    {
        var holderName = new string('A', Card.MaxHolderNameLength);
        var builder = new CardBuilder().WithHolderName(holderName);
        
        var card = builder.Build();
        
        Assert.Equal(holderName, card.HolderName);
    }
    
    [Theory]
    [InlineData("   João Silva   ", "João Silva")]
    [InlineData("\tJoão Silva\t", "João Silva")]
    [InlineData("\nJoão Silva\n", "João Silva")]
    public void Should_Create_Card_With_Trim_HolderName(string inputName, string expectedName)
    {
        var builder = new CardBuilder().WithHolderName(inputName);

        var card = builder.Build();
        
        Assert.Equal(expectedName, card.HolderName);
    }

    [Fact]
    public void Should_Create_Card_When_HolderName_Exceeds_Max_Length_Before_Trim_But_Is_Valid_After()
    {
        var baseHolderName = new string('A', Card.MaxHolderNameLength);
        var inputHolderName = $"     {baseHolderName}     ";
        var builder = new CardBuilder().WithHolderName(inputHolderName);

        var card = builder.Build();
        
        Assert.Equal(baseHolderName, card.HolderName);
    }
    
    [Theory]
    [InlineData(" 1234 ", "1234")]
    [InlineData("\t1234\t", "1234")]
    public void Should_Create_Card_With_Trim_LastDigits(string inputDigits, string expectedDigits)
    {
        var builder = new CardBuilder().WithLastDigits(inputDigits);

        var card = builder.Build();
        
        Assert.Equal(expectedDigits, card.LastDigits);
    }

    [Fact]
    public void Should_Create_Card_When_LastDigits_Exceeds_Max_Length_Before_Trim_But_Is_Valid_After()
    {
        const string inputLastDigits = $"     {CardBuilder.DefaultLastDigits}     ";
        var builder = new CardBuilder().WithLastDigits(inputLastDigits);

        var card = builder.Build();
        
        Assert.Equal(CardBuilder.DefaultLastDigits, card.LastDigits);
    }
    
    // Validation
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void Should_Throw_When_HolderName_Is_Invalid(string? invalidName)
    {
        var builder = new CardBuilder().WithHolderName(invalidName!);

        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);
        
        Assert.Equal("O titular do cartão é obrigatório.", exception.Message);
    }

    [Fact]
    public void Should_Throw_When_HolderName_Exceeds_Max_Length()
    {
        var inputHolderName = new string('A', Card.MaxHolderNameLength + 1);
        var builder = new CardBuilder().WithHolderName(inputHolderName);

        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);
        
        Assert.Equal($"O titular não pode possuir mais de {Card.MaxHolderNameLength} caracteres.", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void Should_Throw_When_LastDigit_Is_Invalid(string? invalidLastDigits)
    {
        var builder = new CardBuilder().WithLastDigits(invalidLastDigits!);

        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);
        
        Assert.Equal("Os últimos dígitos do cartão são obrigatórios.", exception.Message);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("12345")]
    public void Should_Throw_When_LastDigits_Length_Is_Not_Four(string invalidDigits)
    {
        var builder = new CardBuilder().WithLastDigits(invalidDigits);

        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);
        
        Assert.Equal("O cartão deve possuir exatamente quatro dígitos.", exception.Message);
    }

    [Theory]
    [InlineData("123A")]
    [InlineData("!234")]
    [InlineData("1 34")]
    [InlineData("12\u00A04")]
    public void Should_Throw_When_LastDigits_Contain_Non_Digits(string invalidDigits)
    {
        var builder = new CardBuilder().WithLastDigits(invalidDigits);

        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);
        
        Assert.Equal("O cartão deve conter apenas números.", exception.Message);
    }
}