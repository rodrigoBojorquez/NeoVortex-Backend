namespace NeoVortex.Domain.Entities;

public class Book
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int? PageCount { get; set; }
    public int Quantity { get; set; } = 1;
    public DateOnly PublishDate { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}