using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeoVortex.Application.Books.Command.Add;
using NeoVortex.Application.Books.Command.ChangeStock;
using NeoVortex.Application.Books.Command.Update;
using NeoVortex.Application.Books.Command.UploadImage;
using NeoVortex.Application.Books.Queries.List;
using NeoVortex.Application.Common.Files;
using NeoVortex.Application.Common.Results;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Domain.Errors;
using NeoVortex.Infrastructure.Common.Adapters;
using NeoVortex.Infrastructure.Services.Assets;
using NeoVortex.Presentation.Common.Attributes;
using NeoVortex.Presentation.Common.Controllers;

namespace NeoVortex.Presentation.Controllers;

public class BooksController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IBookRepository _bookRepository;

    public BooksController(IMediator mediator, IBookRepository bookRepository)
    {
        _mediator = mediator;
        _bookRepository = bookRepository;
    }

    public record ListBooksRequest(int Page, int PageSize, string? Search, Guid? CategoryId);

    public record CreateBookRequest(
        string Title,
        string Author,
        int Quantity,
        DateOnly PublishDate,
        Guid CategoryId,
        string? Description = null,
        int? PageCount = null);

    public record ChangeStockRequest(Guid Id, int Difference);

    public record UpdateBookRequest(
        Guid Id,
        string Title,
        string Author,
        int Quantity,
        DateOnly PublishDate,
        Guid CategoryId,
        string? Description = null,
        int? PageCount = null);

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> List([FromQuery] ListBooksRequest request)
    {
        var query = new ListBooksQuery(request.Page, request.PageSize, request.Search, request.CategoryId);
        var result = await _mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(Guid id)
    {
        var book = await _bookRepository.IncludeCategoryAsync(id);
        return book is null ? Problem(Errors.Book.NotFound) : Ok(book);
    }

    [HttpPost]
    [RequiredPermission("create:Libros")]
    public async Task<IActionResult> Create(CreateBookRequest request)
    {
        var (title, author, quantity, publishDate, categoryId, description, pageCount) = request;

        var command = new AddBookCommand(
            Title: title,
            Author: author,
            PublishDate: publishDate,
            CategoryId: categoryId,
            Description: description,
            PageCount: pageCount,
            Quantity: quantity
        );

        var result = await _mediator.Send(command);
        return result.Match(created => Ok(created), Problem);
    }

    [HttpPatch]
    [RequiredPermission("update:Libros")]
    public async Task<IActionResult> ChangeStock(ChangeStockRequest request)
    {
        var command = new ChangeBookStockCommand(request.Id, request.Difference);
        var result = await _mediator.Send(command);
        return result.Match(updated => Ok(updated), Problem);
    }

    [HttpPatch("{id}/image")]
    [RequiredPermission("update:Libros")]
    public async Task<IActionResult> UploadImage(Guid id, IFormFile image)
    {
        var command = new UploadBookImageCommand(id, new FormFileAdapter(image));
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, Problem);
    }

    [HttpPut]
    [RequiredPermission("update:Libros")]
    public async Task<IActionResult> Update(UpdateBookRequest request)
    {
        var (id, title, author, quantity, publishDate, categoryId, description, pageCount) = request;

        var command = new UpdateBookCommand(
            Id: id,
            Title: title,
            Author: author,
            PublishDate: publishDate,
            CategoryId: categoryId,
            Description: description,
            PageCount: pageCount,
            Quantity: quantity
        );

        var result = await _mediator.Send(command);

        return result.Match(updated => Ok(updated), Problem);
    }
    
    [HttpDelete("{id}")]
    [RequiredPermission("delete:Libros")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _bookRepository.DeleteAsync(id);
        return Ok();
    }
}