using System.Security.Claims;

namespace Sampark.Authorization;

public interface IAsmAuthorizationService
{
    Task<AsmAuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, string asmCode, CancellationToken cancellationToken);
}
