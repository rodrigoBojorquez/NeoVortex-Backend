namespace NeoVortex.Domain.Entities;

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Content { get; set; }
    public Guid ChatId { get; set; }
    public MessageFrom From { get; set; }
    
    public Chat Chat { get; set; } = null!;
}

public enum MessageFrom
{
    User,
    Assistant
}