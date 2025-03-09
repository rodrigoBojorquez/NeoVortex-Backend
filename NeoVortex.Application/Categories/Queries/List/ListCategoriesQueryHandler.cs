using ErrorOr;
using MediatR;
using NeoVortex.Application.Categories.Common;
using NeoVortex.Application.Common.Results;
using NeoVortex.Application.Interfaces.Repositories;

namespace NeoVortex.Application.Categories.Queries.List;

public class ListCategoriesQueryHandler : IRequestHandler<ListCategoriesQuery, ErrorOr<ListResult<CategoryResult>>>
{
    private readonly ICategoryRepository _categoryRepository;

    public ListCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<ListResult<CategoryResult>>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
    {
        var (page, pageSize, name) = request;

        var data = await _categoryRepository.ListAsync(page, pageSize, c => name == null || c.Name.Contains(name));

        return new ListResult<CategoryResult>(data.Page, data.PageSize, data.TotalItems,
            data.Items.Select(c => new CategoryResult(c.Id, c.Name, c.Description, c.Color)).ToList());
    }
}