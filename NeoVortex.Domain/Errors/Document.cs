using ErrorOr;

namespace NeoVortex.Domain.Errors;

public static partial class Errors
{
    public static class Document
    {
        public static Error NotFound => Error.NotFound("Document.NotFound", "No se encontró el documento.");
    }
}