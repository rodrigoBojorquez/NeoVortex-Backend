using ErrorOr;

namespace NeoVortex.Domain.Errors;

public static partial class Errors
{
    public static class Book
    {
        public static Error NotFound => 
            Error.NotFound("Book.NotFound", "No se encontró el libro.");
    }
}