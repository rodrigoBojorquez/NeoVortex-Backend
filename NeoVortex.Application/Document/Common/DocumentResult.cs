namespace NeoVortex.Application.Document.Common;

public record DocumentResult(Guid Id, string FileName, string Path, DateTime CreatedAt, DateTime UpdatedAt);