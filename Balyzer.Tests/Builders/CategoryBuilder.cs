using Balyzer.Domain.Entities;

namespace Balyzer.Tests.Builders;

public class CategoryBuilder
{
    public const string DefaultName = "Relacionados a Automotivo";

    private string _name = DefaultName;

    public Category Build()
    {
        return new Category(_name);
    }

    public CategoryBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
}