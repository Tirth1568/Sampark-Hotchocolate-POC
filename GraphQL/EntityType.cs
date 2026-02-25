
using HotChocolate.Types;
using HotChocolate.Data;
using Sampark.Models;

namespace Sampark.GraphQL
{
    public class EntityType : ObjectType<Entity>
    {
        protected override void Configure(IObjectTypeDescriptor<Entity> descriptor)
        {
            descriptor.Field(e => e.Children)
                .UseFiltering()
                .UseSorting(); // optional
        }
    }
}



