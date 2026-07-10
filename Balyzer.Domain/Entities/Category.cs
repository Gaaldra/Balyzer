using Balyzer.Domain.Exceptions;

namespace Balyzer.Domain.Entities;

public class Category
{
    public const int MaxNameLength = 100; 
        
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    protected Category()
    {
        
    }

    public Category(string name)
    {
        Id = Guid.CreateVersion7();

        ChangeName(name);
    }

    private void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("O nome da categoria é obrigatório.");

        name = name.Trim();

        if (name.Length > MaxNameLength)
            throw new DomainException($"O nome da categoria não pode possuir mais de {MaxNameLength} caracteres.");

        Name = name;
    }
}