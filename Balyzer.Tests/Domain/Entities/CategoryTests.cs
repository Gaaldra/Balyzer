using Balyzer.Domain.Entities;
using Balyzer.Domain.Exceptions;
using Balyzer.Tests.Domain.Builders;

namespace Balyzer.Tests.Domain.Entities;

public class CategoryTests
{
    // Happy Path

    [Fact]
    public void Should_Create_Category()
    {
        var builder = new CategoryBuilder();

        var category = builder.Build();

        Assert.NotEqual(Guid.Empty, category.Id);
        Assert.Equal(CategoryBuilder.DefaultName, category.Name);
    }

    [Fact]
    public void Should_Create_Category_With_Max_Name_Length()
    {
        var name = new string('A', Category.MaxNameLength);
        var builder = new CategoryBuilder().WithName(name);

        var category = builder.Build();

        Assert.Equal(name, category.Name);
    }

    [Fact]
    public void Should_Create_Category_With_Trim_Name()
    {
        const string name = "    Nova Categoria      ";
        var builder = new CategoryBuilder().WithName(name);

        var category = builder.Build();

        Assert.Equal(name.Trim(), category.Name);
    }

    [Fact]
    public void Should_Create_Category_When_Name_Exceeds_Max_Length_Before_Trim_But_Is_Valid_After()
    {
        var baseName = new string('A', Category.MaxNameLength);
        var inputName = $"    {baseName}        ";
        var builder = new CategoryBuilder().WithName(inputName);

        var category = builder.Build();

        Assert.Equal(baseName, category.Name);
    }

    // Validation

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void Should_Throw_When_Name_Is_Invalid(string? invalidName)
    {
        var builder = new CategoryBuilder().WithName(invalidName!);

        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal("O nome da categoria é obrigatório.", exception.Message);
    }

    [Fact]
    public void Should_Throw_When_Name_Exceeds_Max_Length()
    {
        var name = new string('A', Category.MaxNameLength + 1);
        var builder = new CategoryBuilder().WithName(name);

        var action = builder.Build;

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal($"O nome da categoria não pode possuir mais de {Category.MaxNameLength} caracteres.",
            exception.Message);
    }
}