using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autopartner.Task.Infrastructure.DAL.Entities;
internal class OrderLineEntityConfiguration : IEntityTypeConfiguration<OrderLineEntity>
{
    public void Configure(EntityTypeBuilder<OrderLineEntity> builder)
    {
        builder.ToTable("OrderLines");

        builder.HasKey(x => x.Id);
        builder.Property(p => p.Quantity).IsRequired();
        builder.Property(p => p.Order).IsRequired();
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.Item).IsRequired();

    }
}
