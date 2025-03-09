using ErrorOr;

namespace NeoVortex.Domain.Errors;

public static partial class Errors
{
    public static class Category
    {
        public static Error NotFound =>
            Error.NotFound("Category.NotFound", "La categoría no fue encontrada.");
    }
}