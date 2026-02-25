using Sampark.Data;
using Sampark.Models;

namespace Sampark.GraphQL
{
    public class ProjectType : ObjectType<Project>
    {
        protected override void Configure(IObjectTypeDescriptor<Project> descriptor)
        {
            descriptor
                .Field("activeKaryakarCount")
                .Resolve(ctx =>
                {
                    var project = ctx.Parent<Project>();

                    return project?.Karyakars
                        ?.Count(k => k.Is_Active == true);   // adjust property name
                });
        }

        private class ProjectResolvers
        {
            public int GetActiveKaryakarCount(
                [Parent] Project project,
                [Service] SamparkDbContext db)
            {
                return db.ProjectKaryakars
                    .Count(k => k.ProjectId == project.ProjectId
                             && k.Is_Active == true);
            }
        }
    }
}
