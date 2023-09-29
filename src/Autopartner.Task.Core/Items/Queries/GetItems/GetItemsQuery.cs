using System.Reflection.Metadata.Ecma335;
using Autopartner.Task.Infrastructure.DAL;
using Autopartner.Task.Infrastructure.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Autopartner.Task.Core.Items.Queries.GetItems;

public record GetItemsQuery() : IRequest<ICollection<ItemEntity>>;

internal class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, ICollection<ItemEntity>>
{
    private readonly IMemoryCache _memoryCache;
    private readonly ApplicationContext _context;

    public GetItemsQueryHandler(ApplicationContext context, IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _context = context;
    }

    public async Task<ICollection<ItemEntity>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"{nameof(ItemEntity)}-{nameof(GetItemsQuery)}-All";
        var result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(3);
            var allItems = await _context.Items.AsNoTracking().ToListAsync();
            return allItems;
        })!;
        if (result != null) { return result; }
        else { return new List<ItemEntity>(); }
        return result;
    }
}
