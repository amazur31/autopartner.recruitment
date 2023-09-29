using Autopartner.Task.Core.Items.Models;
using Autopartner.Task.Infrastructure.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Autopartner.Task.Core.Items.Queries.GetItems;

public record GetBudgetsQuery() : IRequest<ICollection<Item>>;

internal class GetItemsQueryHandler : IRequestHandler<GetBudgetsQuery, ICollection<Item>>
{
    private readonly IMemoryCache _memoryCache;
    private readonly ApplicationContext _context;

    public GetItemsQueryHandler(ApplicationContext context, IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _context = context;
    }

    public async Task<ICollection<Item>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"{nameof(Item)}-All";
        await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(3);
            var allItems = await _context.Items.Select(p=>new Item(p)).ToListAsync();
            return allItems;
        })!;
        return new List<Item>();
    }
}
