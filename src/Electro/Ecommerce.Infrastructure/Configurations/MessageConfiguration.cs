using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations;

internal class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(m => m.MessageBy).HasConversion(msgBy => msgBy.ToString(),
            dbMsgBy => (MessageBy)Enum.Parse(typeof(MessageBy), dbMsgBy));

        builder.Property(m => m.MessageType).HasConversion(msgType => msgType.ToString(),
            dbmsgType => (MessageType)Enum.Parse(typeof(MessageType), dbmsgType));
    }
}
