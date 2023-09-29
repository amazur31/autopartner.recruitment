namespace Autopartner.Task.Infrastructure.DAL.Entities;
public class OrderEntity : IBaseEntity
{
    public long Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public ICollection<OrderLineEntity> Lines { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
}
