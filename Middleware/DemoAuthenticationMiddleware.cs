using System.Security.Claims;

namespace Sampark.Middleware;

public sealed class DemoAuthenticationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var userName = context.Request.Headers["x-user"].FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(userName))
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userName)
            };

            var asmCodes = context.Request.Headers["x-asm-codes"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(asmCodes))
            {
                claims.Add(new Claim("x-asm-codes", asmCodes));
            }

            context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType: "HeaderDemo"));
        }

        await next(context);
    }
}
