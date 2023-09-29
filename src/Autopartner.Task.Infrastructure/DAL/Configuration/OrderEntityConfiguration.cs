using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autopartner.Task.Infrastructure.DAL.Entities;
internal class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);
        builder.Property(p => p.AccountNumber).IsRequired();
        builder.Property(p => p.CustomerName).IsRequired();
        builder.Property(p => p.Lines).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();

    }
}
