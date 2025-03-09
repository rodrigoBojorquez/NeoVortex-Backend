using ErrorOr;
using MediatR;

namespace NeoVortex.Application.Books.Command.ChangeStock;

/**
 * Sirve tanto para aumentar como para disminuir el stock de un libro.
 *
 * @param BookId Identificador del libro.
 * @param Difference Diferencia a aplicar al stock actual. Puede ser positiva o negativa.
 */
public record ChangeBookStockCommand(Guid BookId, int Difference) : IRequest<ErrorOr<Updated>>;