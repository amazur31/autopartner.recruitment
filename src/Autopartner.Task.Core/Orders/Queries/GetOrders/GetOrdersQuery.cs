using Autopartner.Task.Infrastructure.DAL.Entities;
using Autopartner.Task.Infrastructure.DAL;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace Autopartner.Task.Core.Orders.Queries.GetOrders;
public record GetOrdersQuery() : IRequest<ICollection<OrderEntity>>;
public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ICollection<OrderEntity>>
{
    private readonly IMemoryCache _memoryCache;
    private readonly ApplicationContext _context;

    public GetOrdersQueryHandler(ApplicationContext context, IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _context = context;
    }

    public async Task<ICollection<OrderEntity>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"{nameof(OrderEntity)}-{nameof(GetOrdersQuery)}-All";
        var result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(3);
            var allItems = await _context.Orders.AsNoTracking().ToListAsync();
            return allItems;
        })!;
        if (result != null) { return result; }
        else { return new List<OrderEntity>(); }
    }
}
