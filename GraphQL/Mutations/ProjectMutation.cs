using HotChocolate;
using HotChocolate.Types;
using Sampark.GraphQL.Inputs;
using Sampark.Models;

namespace Sampark.GraphQL.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class ProjectMutation : GenericMutation<Project, ProjectInput>
    {
        protected override Project MapInputToEntity(ProjectInput input, Project? existingEntity = null)
        {
            var entity = existingEntity ?? new Project
            {
                ProjectUuCode = Guid.NewGuid()
            };

            entity.TemplateId = input.TemplateId;
            entity.Title = input.Title;
            entity.Description = input.Description;
            entity.LocationId = input.LocationId;
            entity.StartDate = input.StartDate;
            entity.EndDate = input.EndDate;
            entity.Tags = input.Tags;
            entity.ReminderFrequency = input.ReminderFrequency;
            entity.ReminderFrequencyConfig = input.ReminderFrequencyConfig;

            return entity;
        }

        protected override void SetEntityId(Project entity, int id)
        {
            entity.ProjectId = id;
        }

        protected override int GetEntityId(Project entity)
        {
            return entity.ProjectId;
        }

        public Task<Project> CreateProject(
            ProjectInput input,
            [Service] Data.SamparkDbContext db,
            [Service] FluentValidation.IValidator<ProjectInput> validator,
            CancellationToken cancellationToken) 
            => Create(input, db, validator, cancellationToken);

        public Task<Project> UpdateProject(
            int id,
            ProjectInput input,
            [Service] Data.SamparkDbContext db,
            [Service] FluentValidation.IValidator<ProjectInput> validator,
            CancellationToken cancellationToken) 
            => Update(id, input, db, validator, cancellationToken);

        public Task<bool> DeleteProject(
            int id,
            [Service] Data.SamparkDbContext db,
            CancellationToken cancellationToken) 
            => Delete(id, db, cancellationToken);
    }
}
