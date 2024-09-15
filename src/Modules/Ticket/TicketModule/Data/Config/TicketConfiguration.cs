using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketModule.Data.Entities;

namespace TicketModule.Data.Config;

internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.OwnerFullName).HasMaxLength(100);
        builder.Property(x => x.PhoneNumber).HasMaxLength(11);
        builder.Property(x => x.Text).HasMaxLength(100);
        builder.Property(x => x.Text).HasMaxLength(3000);
        builder.HasMany(x => x.TicketMessages).WithOne(x => x.Ticket).HasForeignKey(x => x.TicketId);
    }
}

internal class TicketMessageConfiguration:IEntityTypeConfiguration<TicketMessage>
{
    public void Configure(EntityTypeBuilder<TicketMessage> builder)
    {
        builder.Property(x => x.UserFullName).HasMaxLength(100);
        builder.HasKey(x => x.Id);
        
    }
}