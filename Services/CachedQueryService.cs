using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Sampark.Data;
using Sampark.Models;

namespace Sampark.Services;

public interface ICachedQueryService
{
    Task<IReadOnlyList<Person>> GetPersonsCachedAsync(SamparkDbContext db, CancellationToken cancellationToken);
}

public class CachedQueryService(IMemoryCache cache) : ICachedQueryService
{
    private const string PersonsCacheKey = "query:persons:all";

    public async Task<IReadOnlyList<Person>> GetPersonsCachedAsync(SamparkDbContext db, CancellationToken cancellationToken)
    {
        if (cache.TryGetValue(PersonsCacheKey, out IReadOnlyList<Person>? persons) && persons is not null)
        {
            return persons;
        }

        var data = await db.Persons
            .OrderBy(p => p.PersonId)
            .Take(200)
            .ToListAsync(cancellationToken);

        cache.Set(PersonsCacheKey, data, TimeSpan.FromMinutes(5));

        return data;
    }
}
