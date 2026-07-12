using Balyzer.Application.Abstractions.Persistence;
using Balyzer.Application.Categories.Common;

namespace Balyzer.Application.Categories.GetById;

public class GetCategoryByIdHandler(ICategoryRepository categoryRepository)
{
    public async Task<CategoryResponse?> HandlerAsync(
        GetCategoryByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        var category = await categoryRepository.GetByIdAsync(query.Id, cancellationToken);

        return category is null ? null : new CategoryResponse(category.Id, category.Name);
    }
}