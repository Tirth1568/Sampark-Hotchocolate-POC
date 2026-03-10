using HotChocolate;
using HotChocolate.Types;
using Sampark.Data;
using Sampark.GraphQL.Authorization;
using Sampark.Models;

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

        [UseProjection]
        [UseFiltering]
        public IQueryable<Person> PersonsAll([Service] SamparkDbContext db)
            => db.Persons;

        [UsePaging]
        [UseProjection]
        [UseFiltering]
        public IQueryable<Person> PersonsPaged([Service] SamparkDbContext db)
            => db.Persons;
        [UseProjection]
        [UseFiltering]
        [AsmAuthorize("ASM_PROJECT_READ")]
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
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        public IQueryable<Entity> Entities([Service] SamparkDbContext db)
            => db.Entities;

        [UseProjection]
        [UseFiltering]
        public IQueryable<Entity> EntitiesAll([Service] SamparkDbContext db)
            => db.Entities;
    }
}

