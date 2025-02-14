namespace NeoVortex.Domain.Entities;

public class Chat
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public required string Name { get; set; }
    public Guid UserId { get; set; }
    
    public User User { get; set; } = null!;
    public List<Message> Messages { get; set; } = [];
}