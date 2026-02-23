using HotChocolate;
using HotChocolate.Types;
using Sampark.GraphQL.Inputs;
using Sampark.Models;

namespace Sampark.GraphQL.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class EntityMutation : GenericMutation<Entity, EntityInput>
    {
        protected override Entity MapInputToEntity(EntityInput input, Entity? existingEntity = null)
        {
            var entity = existingEntity ?? new Entity
            {
                uuid = Guid.NewGuid().ToString(),
                is_active = 1
            };

            entity.division_id = input.DivisionId;
            entity.code = input.Code;
            entity.name = input.Name;
            entity.parent_entity_id = input.ParentEntityId;
            entity.division_geo_level_id = input.DivisionGeoLevelId;
            entity.phone = input.Phone;
            entity.email = input.Email;
            entity.is_active = input.IsActive;

            return entity;
        }

        protected override void SetEntityId(Entity entity, int id)
        {
            entity.EntityId = id;
        }

        protected override int GetEntityId(Entity entity)
        {
            return entity.EntityId;
        }

        public Task<Entity> CreateEntity(
            EntityInput input,
            [Service] Data.SamparkDbContext db,
            [Service] FluentValidation.IValidator<EntityInput> validator,
            CancellationToken cancellationToken) 
            => Create(input, db, validator, cancellationToken);

        public Task<Entity> UpdateEntity(
            int id,
            EntityInput input,
            [Service] Data.SamparkDbContext db,
            [Service] FluentValidation.IValidator<EntityInput> validator,
            CancellationToken cancellationToken) 
            => Update(id, input, db, validator, cancellationToken);

        public Task<bool> DeleteEntity(
            int id,
            [Service] Data.SamparkDbContext db,
            CancellationToken cancellationToken) 
            => Delete(id, db, cancellationToken);
    }
}
