using ErrorOr;
using MediatR;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Domain.Entities;

namespace NeoVortex.Application.Categories.Commands.Add;

public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, ErrorOr<Created>>
{
    private readonly ICategoryRepository _categoryRepository;

    public AddCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Created>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category()
        {
            Name = request.Name,
            Description = request.Description ?? null,
            Color = request.Color ?? null
        };
        
        await _categoryRepository.InsertAsync(category);

        return Result.Created;
    }
}