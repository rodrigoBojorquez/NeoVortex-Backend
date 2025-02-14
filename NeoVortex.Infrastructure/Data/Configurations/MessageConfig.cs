using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NeoVortex.Domain.Entities;

namespace NeoVortex.Infrastructure.Data.Configurations;

public class MessageConfig : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        var enumConverter = new EnumToStringConverter<MessageFrom>();
        
        builder.Property(m => m.From).HasConversion(enumConverter);
    }
}