using HotChocolate;
using HotChocolate.Types;
using Sampark.GraphQL.Inputs;
using Sampark.Models;

namespace Sampark.GraphQL.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class PersonMutation : GenericMutation<Person, PersonInput>
    {
        protected override Person MapInputToEntity(PersonInput input, Person? existingEntity = null)
        {
            var entity = existingEntity ?? new Person();

            entity.BapsId = input.BapsId;
            entity.BapsPid = input.BapsPid;
            entity.Gender = input.Gender;
            entity.FirstName = input.FirstName;
            entity.MiddleName = input.MiddleName;
            entity.LastName = input.LastName;
            entity.Prefix = input.Prefix;
            entity.Suffix = input.Suffix;
            entity.RecordType = input.RecordType;
            entity.MaritalStatus = input.MaritalStatus;
            entity.DefaultMandalId = input.DefaultMandalId;
            entity.DepartmentId = input.DepartmentId;
            entity.DefaultMandalName = input.DefaultMandalName;
            entity.HomePhoneMasked = input.HomePhoneMasked;
            entity.CellphoneMasked = input.CellphoneMasked;
            entity.EmailMasked = input.EmailMasked;
            entity.AddressMasked = input.AddressMasked;
            entity.StateCode = input.StateCode;
            entity.CountryName = input.CountryName;
            entity.Relation = input.Relation;
            entity.SevaCategory = input.SevaCategory;
            entity.SaveCategoryCode = input.SaveCategoryCode;
            entity.IsPrimary = input.IsPrimary;
            entity.FamilyId = input.FamilyId;
            entity.RelativeTypeId = input.RelativeTypeId;
            entity.RelativeTypeName = input.RelativeTypeName;
            entity.RegionId = input.RegionId;
            entity.RegionName = input.RegionName;
            entity.EntityId = input.EntityId;

            return entity;
        }

        protected override void SetEntityId(Person entity, int id)
        {
            entity.PersonId = id;
        }

        protected override int GetEntityId(Person entity)
        {
            return entity.PersonId;
        }

        public Task<Person> CreatePerson(
            PersonInput input,
            [Service] Data.SamparkDbContext db,
            [Service] FluentValidation.IValidator<PersonInput> validator,
            CancellationToken cancellationToken) 
            => Create(input, db, validator, cancellationToken);

        public Task<Person> UpdatePerson(
            int id,
            PersonInput input,
            [Service] Data.SamparkDbContext db,
            [Service] FluentValidation.IValidator<PersonInput> validator,
            CancellationToken cancellationToken) 
            => Update(id, input, db, validator, cancellationToken);

        public Task<bool> DeletePerson(
            int id,
            [Service] Data.SamparkDbContext db,
            CancellationToken cancellationToken) 
            => Delete(id, db, cancellationToken);
    }
}
