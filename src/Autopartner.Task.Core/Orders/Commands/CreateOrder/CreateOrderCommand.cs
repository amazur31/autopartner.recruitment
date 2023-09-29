using Autopartner.Task.Core.Items.Queries.GetItems;
using Autopartner.Task.Core.OrderLines.Queries.GetLinesByOrderId;
using Autopartner.Task.Core.Orders.Queries.GetOrders;
using Autopartner.Task.Infrastructure.DAL;
using Autopartner.Task.Infrastructure.DAL.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Autopartner.Task.Core.Orders.Commands.CreateOrder;
public class CreateOrderCommand : IRequest<CreateOrderResponse>
{
    public string AccountNumber { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public ICollection<SelectedItem> SelectedItems { get; set; } = null!;
}

public record SelectedItem(long Id, int Quantity);
public record CreateOrderResponse(long OrderId);

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
{
    readonly ApplicationContext _context;
    readonly IMemoryCache _memoryCache;

    public CreateOrderCommandHandler(ApplicationContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }
    public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var currentUtcTime = DateTime.UtcNow;

        var orderToBeCreated = new OrderEntity()
        {
            CreatedAt = currentUtcTime,
            CustomerName = request.CustomerName,
            AccountNumber = request.AccountNumber,
        };

        List<OrderLineEntity> orderLines = new List<OrderLineEntity>();

        foreach (var selection in request.SelectedItems)
        {
            var itemEntity = _context.Items.Single(p=>p.Id == selection.Id);

            OrderLineEntity orderLine = new OrderLineEntity()
            {
                Quantity = selection.Quantity,
                Price = itemEntity.Price,
                ItemId = itemEntity.Id,
                Item = itemEntity,
                CreatedAt = currentUtcTime,
                Order = orderToBeCreated,
            };
        }

        orderToBeCreated.Lines = orderLines;

        var resultOrder = await _context.Orders.AddAsync(orderToBeCreated, cancellationToken);
        _context.SaveChanges();
        
        //Force cache to update next time
        var cacheKey = $"{nameof(OrderEntity)}-{nameof(GetOrdersQuery)}-All";
        _memoryCache.Remove(cacheKey);
        return new(resultOrder.Entity.Id);
    }
}
