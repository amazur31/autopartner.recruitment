using Autopartner.Task.Infrastructure.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Autopartner.Task.Infrastructure.DAL;
public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderLineEntity> OrderLines { get; set; }
    public DbSet<WarehouseEntity> Warehouses { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemEntity>().HasData(
            new ItemEntity
            {
                Id = 1,
                Name = "Some random name",
                Price = 400,
                CreatedAt = DateTimeOffset.UnixEpoch,
            }
        );
        modelBuilder.Entity<WarehouseEntity>().HasData(
            new WarehouseEntity
            {
                Id = 1,
                Available = 5,
                ItemId = 1,
                CreatedAt = DateTimeOffset.UnixEpoch,
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
