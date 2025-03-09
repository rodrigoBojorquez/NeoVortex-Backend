using ErrorOr;

namespace NeoVortex.Domain.Errors;

public static partial class Errors
{
    public static class Chat
    {
        public static Error NotFound => Error.NotFound("Chat.NotFound", "No se encontró el chat.");
    }
}