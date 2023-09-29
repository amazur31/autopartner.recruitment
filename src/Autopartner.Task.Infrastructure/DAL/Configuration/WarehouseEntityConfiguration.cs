using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autopartner.Task.Infrastructure.DAL.Entities;
internal class WarehouseEntityConfiguration : IEntityTypeConfiguration<WarehouseEntity>
{
    public void Configure(EntityTypeBuilder<WarehouseEntity> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);
        builder.Property(p => p.Item).IsRequired();
        builder.Property(p => p.Available).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();

    }
}
