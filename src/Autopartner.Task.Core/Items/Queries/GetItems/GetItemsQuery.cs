using Autopartner.Task.Infrastructure.DAL;
using Autopartner.Task.Infrastructure.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Autopartner.Task.Core.Items.Queries.GetItems;

public record GetBudgetsQuery() : IRequest<ICollection<ItemEntity>>;

internal class GetItemsQueryHandler : IRequestHandler<GetBudgetsQuery, ICollection<ItemEntity>>
{
    private readonly IMemoryCache _memoryCache;
    private readonly ApplicationContext _context;

    public GetItemsQueryHandler(ApplicationContext context, IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _context = context;
    }

    public async Task<ICollection<ItemEntity>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"{nameof(ItemEntity)}-{nameof(GetBudgetsQuery)}-All";
        await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(3);
            var allItems = await _context.Items.AsNoTracking().ToListAsync();
            return allItems;
        })!;
        return new List<ItemEntity>();
    }
}
