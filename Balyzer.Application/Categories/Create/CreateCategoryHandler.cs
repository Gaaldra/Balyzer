using Balyzer.Application.Abstractions.Persistence;
using Balyzer.Domain.Entities;

namespace Balyzer.Application.Categories.Create;

public class CreateCategoryHandler(ICategoryRepository categoryRepository)
{
    public async Task<Guid> HandlerAsync(
        CreateCategoryCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var category = new Category(command.Name);

        await categoryRepository.AddAsync(category, cancellationToken);

        return category.Id;
    }
}