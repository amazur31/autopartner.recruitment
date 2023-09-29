using Autopartner.Task.Infrastructure.DAL.Entities;

namespace Autopartner.Task.Core.Items.Models;
public class Item
{
    public Item()
    {

    }

    public Item(ItemEntity itemEntity)
    {
        Id= itemEntity.Id;
        Name= itemEntity.Name;
        Price= itemEntity.Price;
        CreatedAt= itemEntity.CreatedAt;
        ModifiedAt= itemEntity.ModifiedAt;
    }
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
}
