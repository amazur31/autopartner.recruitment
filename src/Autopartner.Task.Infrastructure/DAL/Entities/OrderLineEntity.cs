namespace Autopartner.Task.Infrastructure.DAL.Entities;
public class OrderLineEntity : IBaseEntity
{
    public long Id { get; set; }
    public OrderEntity Order { get; set; } = null!;
    public long OrderId { get; set; }
    public ItemEntity Item { get; set; } = null!;
    public long ItemId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
}
