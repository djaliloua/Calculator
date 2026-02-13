using Calculator.SettingsLayer.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Broker;

namespace Calculator.Auth;

public class AuthenticationClient : IAuthService
{
    private readonly ISettingsManager _settings;
    private readonly ILogger<AuthenticationClient>? _logger;
    private readonly IPublicClientApplication _publicClientApp;
    private readonly string[] _scopes;
    private AuthResult _current;

    public AuthResult Current => _current;

    public AuthenticationClient(ISettingsManager settings, ILogger<AuthenticationClient>? logger = null)
    {
        _settings = settings;
        _logger = logger;

        var clientId = GetSetting("MsalClientId", "client_id");
        var tenantId = GetSetting("MsalTenantId", "common");
        var redirectUri = GetSetting("MsalRedirectUri", "http://localhost");
        var scopesSetting = GetSetting("MsalScopes", "User.Read");
        var useBroker = bool.Parse(GetSetting("MsalUseBroker", "false"));

        _scopes = scopesSetting.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var builder = PublicClientApplicationBuilder
            .Create(clientId)
            .WithRedirectUri(redirectUri)
            .WithAuthority(AzureCloudInstance.AzurePublic, tenantId);

        // Only use broker if explicitly enabled and on Windows 10+
        if (useBroker && IsWindows10OrGreater())
        {
            builder.WithBroker(new BrokerOptions(BrokerOptions.OperatingSystems.Windows)
            {
                Title = "Calculator Login",
                ListOperatingSystemAccounts = true
            });
        }

        _publicClientApp = builder.Build();
        _current = AuthResult.Empty;
    }

    public async Task<AuthResult> LoginAsync()
    {
        try
        {
            // Try silent authentication first
            var accounts = await _publicClientApp.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();

            AuthenticationResult? authResult = null;

            if (firstAccount != null)
            {
                try
                {
                    authResult = await _publicClientApp
                        .AcquireTokenSilent(_scopes, firstAccount)
                        .ExecuteAsync();

                    _logger?.LogInformation("Silent token acquisition succeeded for {UserName}", authResult.Account.Username);
                }
                catch (MsalUiRequiredException ex)
                {
                    _logger?.LogInformation("Silent acquisition failed, requiring interactive login: {ErrorCode}", ex.ErrorCode);
                    // Silent acquisition failed, fall back to interactive
                    authResult = null;
                }
            }

            // If silent authentication didn't work, try interactive
            if (authResult == null)
            {
                authResult = await AcquireTokenInteractiveAsync();
            }

            _current = MapToAuthResult(authResult);
            _logger?.LogInformation("Login succeeded for {UserName}", _current.UserName);
            return _current;
        }
        catch (MsalServiceException ex)
        {
            _logger?.LogError(ex, "MSAL service error: {ErrorCode} - {Message}", ex.ErrorCode, ex.Message);
            return _current;
        }
        catch (MsalClientException ex)
        {
            _logger?.LogError(ex, "MSAL client error: {ErrorCode} - {Message}", ex.ErrorCode, ex.Message);
            return _current;
        }
        catch (MsalException ex)
        {
            _logger?.LogError(ex, "MSAL authentication failed: {ErrorCode}", ex.ErrorCode);
            return _current;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Unexpected error during authentication");
            return _current;
        }
    }

    public async Task LogoutAsync()
    {
        if (!_current.IsAuthenticated)
        {
            return;
        }

        try
        {
            var accounts = await _publicClientApp.GetAccountsAsync();
            foreach (var account in accounts)
            {
                await _publicClientApp.RemoveAsync(account);
            }

            _current = AuthResult.Empty;
            _logger?.LogInformation("Logout completed");
        }
        catch (MsalException ex)
        {
            _logger?.LogError(ex, "Logout failed: {ErrorCode}", ex.ErrorCode);
        }
    }

    private async Task<AuthenticationResult> AcquireTokenInteractiveAsync()
    {
        var builder = _publicClientApp
            .AcquireTokenInteractive(_scopes)
            .WithPrompt(Prompt.SelectAccount);

        // Only set parent window if we have a valid window handle
        var windowHandle = GetParentWindow();
        if (windowHandle != IntPtr.Zero)
        {
            builder = builder.WithParentActivityOrWindow(windowHandle);
        }

        return await builder.ExecuteAsync();
    }

    private static AuthResult MapToAuthResult(AuthenticationResult authResult)
    {
        return new AuthResult(
            IsAuthenticated: true,
            UserName: authResult.Account?.Username ?? "Unknown",
            AccessToken: authResult.AccessToken ?? string.Empty,
            IdentityToken: authResult.IdToken ?? string.Empty,
            ExpiresAt: authResult.ExpiresOn
        );
    }

    private string GetSetting(string key, string fallback)
    {
        var value = _settings.GetParameter(key)?.ToString();
        return string.IsNullOrWhiteSpace(value) ? fallback : value;
    }

    private static IntPtr GetParentWindow()
    {
        try
        {
            if (System.Windows.Application.Current?.MainWindow != null)
            {
                return new System.Windows.Interop.WindowInteropHelper(
                    System.Windows.Application.Current.MainWindow).Handle;
            }
        }
        catch (Exception)
        {
            // Swallow exception if window is not available
        }

        return IntPtr.Zero;
    }

    private static bool IsWindows10OrGreater()
    {
        try
        {
            return Environment.OSVersion.Version.Major >= 10;
        }
        catch
        {
            return false;
        }
    }
}


