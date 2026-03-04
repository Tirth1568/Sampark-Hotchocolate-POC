using FluentValidation;
using HotChocolate;

namespace Sampark.GraphQL.Errors;

public sealed class ValidationExceptionErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is not ValidationException validationException)
        {
            return error;
        }

        var validationErrors = validationException.Errors
            .Select(e => new
            {
                field = e.PropertyName,
                message = e.ErrorMessage,
                attemptedValue = e.AttemptedValue
            })
            .ToArray();

        return error
            .WithMessage("Validation failed for the request.")
            .SetExtension("code", "VALIDATION_ERROR")
            .SetExtension("httpStatus", StatusCodes.Status400BadRequest)
            .SetExtension("validationErrors", validationErrors);
    }
}
