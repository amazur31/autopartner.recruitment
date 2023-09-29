namespace Autopartner.Task.Infrastructure.DAL.Entities;
public class WarehouseEntity : IBaseEntity
{
    public long Id { get; set; }
    public ItemEntity Item { get; set; } = null!;
    public long ItemId { get; set; }
    public int Available { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
}
