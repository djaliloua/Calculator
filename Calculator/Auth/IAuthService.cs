namespace Calculator.Auth;

public interface IAuthService
{
    AuthResult Current { get; }
    Task<AuthResult> LoginAsync();
    Task LogoutAsync();
}