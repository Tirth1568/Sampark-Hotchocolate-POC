using FluentValidation;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Sampark.Data;

namespace Sampark.GraphQL
{
    public abstract class GenericMutation<TEntity, TInput> 
        where TEntity : class, new()
        where TInput : class
    {
        protected abstract TEntity MapInputToEntity(TInput input, TEntity? existingEntity = null);
        protected abstract void SetEntityId(TEntity entity, int id);
        protected abstract int GetEntityId(TEntity entity);

        public async Task<TEntity> Create(
            TInput input,
            [Service] SamparkDbContext db,
            [Service] IValidator<TInput> validator,
            CancellationToken cancellationToken)
        {
            // Validate input
            var validationResult = await validator.ValidateAsync(input, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map input to entity
            var entity = MapInputToEntity(input);

            // Add to database
            db.Set<TEntity>().Add(entity);
            await db.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<TEntity> Update(
            int id,
            TInput input,
            [Service] SamparkDbContext db,
            [Service] IValidator<TInput> validator,
            CancellationToken cancellationToken)
        {
            // Validate input
            var validationResult = await validator.ValidateAsync(input, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Find existing entity
            var entity = await db.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
            if (entity == null)
            {
                throw new Exception($"Entity with ID {id} not found");
            }

            // Map input to existing entity
            entity = MapInputToEntity(input, entity);

            // Update in database
            db.Set<TEntity>().Update(entity);
            await db.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<bool> Delete(
            int id,
            [Service] SamparkDbContext db,
            CancellationToken cancellationToken)
        {
            var entity = await db.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
            if (entity == null)
            {
                throw new Exception($"Entity with ID {id} not found");
            }

            db.Set<TEntity>().Remove(entity);
            await db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
