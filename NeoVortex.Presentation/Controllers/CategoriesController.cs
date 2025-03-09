using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeoVortex.Application.Categories.Commands.Add;
using NeoVortex.Application.Categories.Commands.Update;
using NeoVortex.Application.Categories.Queries.List;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Domain.Errors;
using NeoVortex.Presentation.Common.Attributes;
using NeoVortex.Presentation.Common.Controllers;

namespace NeoVortex.Presentation.Controllers;

public class CategoriesController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesController(IMediator mediator, ICategoryRepository categoryRepository)
    {
        _mediator = mediator;
        _categoryRepository = categoryRepository;
    }

    public record ListCategoriesRequest(int Page, int PageSize, string? Search);

    public record CreateCategoryRequest(string Name, string? Description, string? Color);

    public record UpdateCategoryRequest(Guid Id, string Name, string? Description, string? Color);

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCategories([FromQuery] ListCategoriesRequest request)
    {
        var query = new ListCategoriesQuery(request.Page, request.PageSize, request.Search);
        var result = await _mediator.Send(query);

        return result.Match<IActionResult>(Ok, Problem);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null) return Problem(Errors.Category.NotFound);

        return Ok(category);
    }

    [HttpPost]
    [RequiredPermission("create:Categorias")]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
    {
        var command = new AddCategoryCommand(request.Name, request.Description, request.Color);
        var result = await _mediator.Send(command);

        return result.Match(created => Ok(created), Problem);
    }

    [HttpPut]
    [RequiredPermission("update:Categorias")]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryRequest request)
    {
        var command = new UpdateCategoryCommand(request.Id, request.Name, request.Description, request.Color);
        var result = await _mediator.Send(command);

        return result.Match(updated => Ok(updated), Problem);
    }

    [HttpDelete]
    [RequiredPermission("delete:Categorias")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        await _categoryRepository.DeleteAsync(id);
        return Ok(Result.Deleted);
    }
}