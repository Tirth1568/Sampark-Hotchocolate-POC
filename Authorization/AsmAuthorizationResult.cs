namespace Sampark.Authorization;

public sealed record AsmAuthorizationResult(bool IsAuthorized, string? FailureReason = null);
