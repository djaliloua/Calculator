namespace Calculator.Auth;

public sealed record AuthResult(
    bool IsAuthenticated,
    string UserName,
    string AccessToken,
    string IdentityToken,
    DateTimeOffset ExpiresAt)
{
    public static AuthResult Empty { get; } =
        new(false, "Guest", string.Empty, string.Empty, DateTimeOffset.MinValue);
}