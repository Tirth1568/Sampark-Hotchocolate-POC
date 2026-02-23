using FluentValidation;
using Sampark.GraphQL.Inputs;

namespace Sampark.GraphQL.Validators
{
    public class ProjectValidator : AbstractValidator<ProjectInput>
    {
        public ProjectValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("Title is required and must not exceed 200 characters");

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("Description must not exceed 1000 characters");

            RuleFor(x => x.TemplateId)
                .GreaterThan(0)
                .WithMessage("Template ID must be greater than 0");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("End date must be greater than or equal to start date");
        }
    }
}
