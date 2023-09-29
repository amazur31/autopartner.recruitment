using Autopartner.Task.Core.Items.Queries.GetItems;
using Autopartner.Task.Infrastructure.DAL.Entities;
using Autopartner.Task.Infrastructure.DAL;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace Autopartner.Task.Core.OrderLines.Queries.GetLinesByOrderId;
public record GetLinesByOrderIdQuery(long OrderId) : IRequest<ICollection<OrderLineEntity>>;
public class GetLinesByOrderIdQueryHandler : IRequestHandler<GetLinesByOrderIdQuery, ICollection<OrderLineEntity>>
{
    private readonly IMemoryCache _memoryCache;
    private readonly ApplicationContext _context;

    public GetLinesByOrderIdQueryHandler(ApplicationContext context, IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _context = context;
    }

    public async Task<ICollection<OrderLineEntity>> Handle(GetLinesByOrderIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"{nameof(OrderLineEntity)}-{nameof(GetLinesByOrderIdQuery)}-{request.OrderId}";
        var result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(3);
            var allItems = await _context.OrderLines.AsNoTracking().Where(p=>p.OrderId == request.OrderId).ToListAsync();
            return allItems;
        })!;
        if (result != null) { return result; }
        else { return new List<OrderLineEntity>(); }
    }
}
