using Calculator.SettingsLayer.Abstractions;
using Duende.IdentityModel.OidcClient;
using Duende.IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Calculator.Auth;

public class SystemBrowser : IBrowser
{
    private readonly int? _port;

    // Optional: Pass a specific port, or leave null for a random available one
    public SystemBrowser(int? port = null)
    {
        _port = port;
    }

    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        // 1. Determine the port to use
        int assignedPort = _port ?? GetRandomUnusedPort();

        // 2. Setup the listener on the assigned port
        using var listener = new HttpListener();
        string listenerPrefix = $"http://127.0.0.1:{assignedPort}/";
        listener.Prefixes.Add(listenerPrefix);
        listener.Start();

        // 3. Open the system browser
        Process.Start(new ProcessStartInfo
        {
            FileName = options.StartUrl,
            UseShellExecute = true
        });

        // 4. Wait for Keycloak to redirect back
        var context = await listener.GetContextAsync();

        // Send a "Success" message to the user in the browser
        string responseString = "<html><body style='font-family:sans-serif; text-align:center; padding-top:50px;'>" +
                                "<h1>Login Successful!</h1><p>You can now close this window and return to the app.</p>" +
                                "</body></html>";
        var buffer = Encoding.UTF8.GetBytes(responseString);
        context.Response.ContentLength64 = buffer.Length;
        await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
        context.Response.OutputStream.Close();

        listener.Stop();

        return new BrowserResult
        {
            Response = context.Request.Url.ToString(),
            ResultType = BrowserResultType.Success
        };
    }

    private static int GetRandomUnusedPort()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        int port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }
}


public sealed class KeycloakAuthService : IAuthService
{
    private readonly ISettingsManager _settings;
    private readonly ILogger<KeycloakAuthService> _logger;
    private readonly OidcClient _oidcClient;
    private AuthResult _current;

    public AuthResult Current => _current;

    public KeycloakAuthService(ISettingsManager settings, ILogger<KeycloakAuthService> logger)
    {
        _settings = settings;
        _logger = logger;

        var baseUrl = GetSetting("KeycloakBaseUrl", "https://localhost:8080");
        var realm = GetSetting("KeycloakRealm", "my_realm");
        var clientId = GetSetting("KeycloakClientId", "m2m-application");
        var clientSecret = GetSetting("KeycloakClientSecret", string.Empty);
        var redirectUri = GetSetting("KeycloakRedirectUri", "http://127.0.0.1:7890/callback");
        var scope = GetSetting("KeycloakScope", "openid profile");

        var authority = $"{baseUrl.TrimEnd('/')}/realms/{realm}";
        var redirectPort = new Uri(redirectUri).Port;
        var browser = new SystemBrowser(redirectPort);
        var options = new OidcClientOptions
        {
            Authority = authority,
            ClientId = clientId,
            RedirectUri = redirectUri,
            Scope = scope,
            Browser = browser,
            Policy = new Policy
            {
                RequireAccessTokenHash = false
            },
            DisablePushedAuthorization = true
        };

        if (!string.IsNullOrWhiteSpace(clientSecret))
        {
            options.ClientSecret = clientSecret;
        }

        _oidcClient = new OidcClient(options);
        _current = AuthResult.Empty;
    }

    public async Task<AuthResult> LoginAsync()
    {
        var result = await _oidcClient.LoginAsync(new LoginRequest());

        if (result.IsError)
        {
            _logger.LogWarning("Keycloak login failed: {Error}", result.Error);
            return _current;
        }

        var userName = result.User?.Identity?.Name
            ?? result.User?.FindFirst("preferred_username")?.Value
            ?? "User";

        _current = new AuthResult(
            true,
            userName,
            result.AccessToken ?? string.Empty,
            result.IdentityToken ?? string.Empty,
            result.AccessTokenExpiration);

        _logger.LogInformation("Keycloak login succeeded for {UserName}", userName);
        return _current;
    }

    public async Task LogoutAsync()
    {
        if (!_current.IsAuthenticated)
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(_current.IdentityToken))
        {
            await _oidcClient.LogoutAsync(new LogoutRequest
            {
                IdTokenHint = _current.IdentityToken
            });
        }

        _current = AuthResult.Empty;
        _logger.LogInformation("Keycloak logout completed.");
    }

    private string GetSetting(string key, string fallback)
    {
        var value = _settings.GetParameter(key)?.ToString();
        return string.IsNullOrWhiteSpace(value) ? fallback : value;
    }
}