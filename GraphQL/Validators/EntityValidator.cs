using FluentValidation;
using Sampark.GraphQL.Inputs;

namespace Sampark.GraphQL.Validators
{
    public class EntityValidator : AbstractValidator<EntityInput>
    {
        public EntityValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("Entity name is required and must not exceed 200 characters");

            RuleFor(x => x.Code)
                .MaximumLength(100)
                .WithMessage("Entity code must not exceed 100 characters");

            RuleFor(x => x.ParentEntityId)
                .GreaterThan(0)
                .When(x => x.ParentEntityId.HasValue)
                .WithMessage("Parent entity ID must be greater than 0 when provided");

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Invalid email format");
        }
    }
}
