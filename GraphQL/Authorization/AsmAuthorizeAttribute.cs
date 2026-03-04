using System.Reflection;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types.Descriptors;
using Sampark.Authorization;

namespace Sampark.GraphQL.Authorization;

[AttributeUsage(AttributeTargets.Method)]
public sealed class AsmAuthorizeAttribute(string asmCode) : ObjectFieldDescriptorAttribute
{
    protected override void OnConfigure(
        IDescriptorContext context,
        IObjectFieldDescriptor descriptor,
        MemberInfo member)
    {
        descriptor.Use(next => async fieldContext =>
        {
            var authService = fieldContext.Service<IAsmAuthorizationService>();
            var httpContextAccessor = fieldContext.Service<IHttpContextAccessor>();
            var httpContext = httpContextAccessor.HttpContext
                ?? throw new GraphQLException(
                    ErrorBuilder.New()
                        .SetMessage("HTTP context is unavailable.")
                        .SetCode("INTERNAL_SERVER_ERROR")
                        .Build());

            var authorization = await authService.AuthorizeAsync(httpContext.User, asmCode, fieldContext.RequestAborted);
            if (!authorization.IsAuthorized)
            {
                throw new GraphQLException(
                    ErrorBuilder.New()
                        .SetMessage(authorization.FailureReason ?? "Access denied.")
                        .SetCode(httpContext.User.Identity?.IsAuthenticated == true ? "ASM_FORBIDDEN" : "AUTH_NOT_AUTHENTICATED")
                        .SetExtension("requiredAsmCode", asmCode)
                        .Build());
            }

            await next(fieldContext);
        });
    }
}
