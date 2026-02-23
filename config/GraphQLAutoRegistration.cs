using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Sampark.config
{
    using HotChocolate.Types;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public static class GraphQLAutoRegistration
    {
        public static IRequestExecutorBuilder AddDbSetQueries<TDbContext>(
            this IRequestExecutorBuilder builder)
            where TDbContext : DbContext
        {
            var dbSetProps = typeof(TDbContext)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToList();

            foreach (var prop in dbSetProps)
            {
                var entityType = prop.PropertyType.GetGenericArguments()[0];
                var method = typeof(GraphQLAutoRegistration)
                    .GetMethod(nameof(RegisterEntity), BindingFlags.NonPublic | BindingFlags.Static)!
                    .MakeGenericMethod(typeof(TDbContext), entityType);

                builder = (IRequestExecutorBuilder)method.Invoke(null, new object[] { builder, prop.Name })!;
            }

            return builder;
        }

        private static IRequestExecutorBuilder RegisterEntity<TDbContext, TEntity>(
            IRequestExecutorBuilder builder,
            string dbSetName)
            where TDbContext : DbContext
            where TEntity : class
        {
            return builder.AddTypeExtension(new ObjectTypeExtension(descriptor =>
            {
                descriptor.Name(OperationTypeNames.Query);

                descriptor
                    .Field(dbSetName.ToLower())   // uses DbSet name
                    .Type<ListType<ObjectType<TEntity>>>()
                    .Resolve(ctx =>
                    {
                        var db = ctx.Service<TDbContext>();
                        return db.Set<TEntity>();  // strongly typed
                    })
                    .UseProjection()
                    .UseFiltering()
                    .UseSorting();
            }));
        }
    }

}

