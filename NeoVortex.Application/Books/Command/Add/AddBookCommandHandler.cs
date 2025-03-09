using ErrorOr;
using MediatR;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Domain.Entities;

namespace NeoVortex.Application.Books.Command.Add;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand, ErrorOr<Created>>
{
    private readonly IBookRepository _bookRepository;

    public AddBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<ErrorOr<Created>> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            CategoryId = request.CategoryId,
            Description = request.Description,
            PublishDate = request.PublishDate,
            PageCount = request.PageCount,
            Quantity = request.Quantity ?? 1
        };

        await _bookRepository.InsertAsync(book);

        return Result.Created;
    }
}