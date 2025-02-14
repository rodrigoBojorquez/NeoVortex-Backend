namespace NeoVortex.Domain.Entities;

public class Document
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public required string FileName { get; set; }
    public required string Path { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid UserId { get; set; }
    
    public User User { get; set; } = null!;
}