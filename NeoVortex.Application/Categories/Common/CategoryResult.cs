namespace NeoVortex.Application.Categories.Common;

public record CategoryResult(Guid Id, string Name, string? Description = null, string? Color = null);