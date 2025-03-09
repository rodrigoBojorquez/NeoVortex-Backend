using ErrorOr;

namespace NeoVortex.Domain.Errors;

public static partial class Errors
{
    public static class Message
    {
        public static Error NotFound => Error.NotFound("Message.NotFound", "No se encontró el mensaje.");
    }
}