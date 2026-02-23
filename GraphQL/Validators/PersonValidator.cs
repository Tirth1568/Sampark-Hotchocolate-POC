using FluentValidation;
using Sampark.GraphQL.Inputs;

namespace Sampark.GraphQL.Validators
{
    public class PersonValidator : AbstractValidator<PersonInput>
    {
        public PersonValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("First name is required and must not exceed 100 characters");

            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .WithMessage("Last name must not exceed 100 characters");

            RuleFor(x => x.Gender)
                .MaximumLength(20)
                .WithMessage("Gender must not exceed 20 characters");

            RuleFor(x => x.EmailMasked)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.EmailMasked))
                .WithMessage("Invalid email format");

            RuleFor(x => x.BapsId)
                .MaximumLength(50)
                .WithMessage("BAPS ID must not exceed 50 characters");
        }
    }
}
