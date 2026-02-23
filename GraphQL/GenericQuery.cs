using HotChocolate;
using HotChocolate.Types;
using Sampark.Data;
using Sampark.Models;
using Sampark.Services;

namespace Sampark.GraphQL
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class GenericQuery<T> where T : class
    {
        public IQueryable<T> Get([Service] SamparkDbContext db)
            => db.Set<T>();
    }
    [ExtendObjectType(OperationTypeNames.Query)]
    public class TestQuery
    {
        public IQueryable<Person> Persons([Service] SamparkDbContext db)
            => db.Persons;

        public async Task<IReadOnlyList<Person>> PersonsCached(
            [Service] SamparkDbContext db,
            [Service] ICachedQueryService cachedQueryService,
            CancellationToken cancellationToken)
            => await cachedQueryService.GetPersonsCachedAsync(db, cancellationToken);
        [UseProjection]
        [UseFiltering]
        public IQueryable<Project> Projects([Service] SamparkDbContext db)
            => db.Projects;
        [UseProjection]
        [UseFiltering]
        public IQueryable<ProjectFamily> ProjectFamilies([Service] SamparkDbContext db)
            => db.ProjectFamilies;
        [UseProjection]
        [UseFiltering]
        public IQueryable<ProjectKaryakarPair> ProjectKaryakarPairs([Service] SamparkDbContext db)
            => db.ProjectKaryakarPairs;
        [UseProjection]
        [UseFiltering]
        public IQueryable<ProjectKaryakar> ProjectKaryakars([Service] SamparkDbContext db)
            => db.ProjectKaryakars;
        [UseProjection]
        [UseFiltering]
        public IQueryable<Entity> Entities([Service] SamparkDbContext db)
            => db.Entities;
    }
}

