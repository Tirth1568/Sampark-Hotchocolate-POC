using System.Security.Claims;

namespace Sampark.Authorization;

/// <summary>
/// Demo service that simulates an external authorization system.
/// Allowed ASM codes are passed through the "x-asm-codes" claim (comma separated).
/// </summary>
public sealed class DemoExternalAsmAuthorizationService : IAsmAuthorizationService
{
    public async Task<AsmAuthorizationResult> AuthorizeAsync(
        ClaimsPrincipal user,
        string asmCode,
        CancellationToken cancellationToken)
    {
        // Simulate network call latency to an external policy system.
        await Task.Delay(TimeSpan.FromMilliseconds(40), cancellationToken);

        if (user.Identity is null || !user.Identity.IsAuthenticated)
        {
            return new AsmAuthorizationResult(false, "User is not authenticated.");
        }

        var rawCodes = user.FindFirst("x-asm-codes")?.Value;
        if (string.IsNullOrWhiteSpace(rawCodes))
        {
            return new AsmAuthorizationResult(false, "No ASM codes were provided for the current user.");
        }

        var userCodes = rawCodes
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        return userCodes.Contains(asmCode)
            ? new AsmAuthorizationResult(true)
            : new AsmAuthorizationResult(false, $"ASM code '{asmCode}' is required.");
    }
}
