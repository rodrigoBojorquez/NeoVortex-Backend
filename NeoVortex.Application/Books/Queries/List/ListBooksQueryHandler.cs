using ErrorOr;
using MediatR;
using NeoVortex.Application.Books.Common;
using NeoVortex.Application.Common.Results;
using NeoVortex.Application.Interfaces.Repositories;

namespace NeoVortex.Application.Books.Queries.List;

public class ListBooksQueryHandler : IRequestHandler<ListBooksQuery, ErrorOr<ListResult<BookResult>>>
{
    private readonly IBookRepository _bookRepository;

    public ListBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<ErrorOr<ListResult<BookResult>>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
    {
        var (page, pageSize, title, categoryId) = request;

        var data = await _bookRepository.ListAsync(page, pageSize, b => 
            (title == null || b.Title.Contains(title)) &&
            (categoryId == null || b.CategoryId == categoryId));
        
        return new ListResult<BookResult>(data.Page, data.PageSize, data.TotalItems,
            data.Items.Select(b => new BookResult(
                b.Id, 
                b.Title, 
                b.Author, 
                b.Description ?? string.Empty, 
                b.ImageUrl ?? string.Empty, 
                b.Category.Name, 
                b.CategoryId, 
                b.PublishDate, 
                b.CreateDate, 
                b.Quantity, 
                b.PageCount ?? 0)).ToList());
    }
}