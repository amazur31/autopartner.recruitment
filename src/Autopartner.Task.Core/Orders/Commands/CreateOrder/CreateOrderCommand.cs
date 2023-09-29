using Autopartner.Task.Infrastructure.DAL;
using Autopartner.Task.Infrastructure.DAL.Entities;
using MediatR;

namespace Autopartner.Task.Core.Orders.Commands.CreateOrder;
public record CreateOrderCommand(string AccountNumber, string CustomerName, ICollection<SelectedItem> SelectedItems) : IRequest<CreateOrderResponse>;

public record SelectedItem(long Id, int Quantity)
public record CreateOrderResponse(long OrderId);

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
{
    readonly ApplicationContext _context;
    public CreateOrderCommandHandler(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var currentUtcTime = DateTime.UtcNow;
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
            };
        }
        var orderToBeCreated = new OrderEntity()
        {
            CreatedAt = currentUtcTime,
            CustomerName = request.CustomerName,
            AccountNumber = request.AccountNumber,
            Lines = orderLines
        };

        var result = await _context.Orders.AddAsync(orderToBeCreated, cancellationToken);

        return new(result.Entity.Id);
    }
}
