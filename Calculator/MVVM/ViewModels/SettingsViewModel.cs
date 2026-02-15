using Calculator.SettingsLayer.Abstractions;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace Calculator.MVVM.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly ISettingsManager _settings;
        private readonly ILogger<SettingsViewModel> _logger;
        private readonly PaletteHelper _paletteHelper;

        #region Theme Settings
        private bool _isDark;
        public bool IsDark
        {
            get => _isDark;
            set => UpdateObservable(ref _isDark, value, () =>
            {
                SetTheme(IsDark);
                _settings.SetParameter(nameof(IsDark), value);
                _logger?.LogInformation("Theme changed to {Theme}", value ? "Dark" : "Light");
            });
        }
        #endregion

        #region Calculator Settings
        private int _countThreshold;
        public int CountThreshold
        {
            get => _countThreshold;
            set => UpdateObservable(ref _countThreshold, value, () =>
            {
                _settings.SetParameter(nameof(CountThreshold), value);
                _logger?.LogInformation("CountThreshold changed to {Value}", value);
            });
        }
        #endregion

        #region Authentication Provider Settings
        private string _authProvider;
        public string AuthProvider
        {
            get => _authProvider;
            set => UpdateObservable(ref _authProvider, value);
        }

        // MSAL Settings
        private string _msalClientId;
        public string MsalClientId
        {
            get => _msalClientId;
            set => UpdateObservable(ref _msalClientId, value);
        }
        public ObservableCollection<string> AuthProviders { get; } = new ObservableCollection<string> { "msal", "keycloak" };
        private string _msalTenantId;
        public string MsalTenantId
        {
            get => _msalTenantId;
            set => UpdateObservable(ref _msalTenantId, value);
        }

        private string _msalRedirectUri;
        public string MsalRedirectUri
        {
            get => _msalRedirectUri;
            set => UpdateObservable(ref _msalRedirectUri, value);
        }

        private string _msalScopes;
        public string MsalScopes
        {
            get => _msalScopes;
            set => UpdateObservable(ref _msalScopes, value);
        }

        private bool _msalUseBroker;
        public bool MsalUseBroker
        {
            get => _msalUseBroker;
            set => UpdateObservable(ref _msalUseBroker, value);
        }

        // Keycloak Settings
        private string _keycloakBaseUrl;
        public string KeycloakBaseUrl
        {
            get => _keycloakBaseUrl;
            set => UpdateObservable(ref _keycloakBaseUrl, value);
        }

        private string _keycloakRealm;
        public string KeycloakRealm
        {
            get => _keycloakRealm;
            set => UpdateObservable(ref _keycloakRealm, value);
        }

        private string _keycloakClientId;
        public string KeycloakClientId
        {
            get => _keycloakClientId;
            set => UpdateObservable(ref _keycloakClientId, value);
        }

        private string _keycloakClientSecret;
        public string KeycloakClientSecret
        {
            get => _keycloakClientSecret;
            set => UpdateObservable(ref _keycloakClientSecret, value);
        }

        private string _keycloakRedirectUri;
        public string KeycloakRedirectUri
        {
            get => _keycloakRedirectUri;
            set => UpdateObservable(ref _keycloakRedirectUri, value);
        }

        private string _keycloakScope;
        public string KeycloakScope
        {
            get => _keycloakScope;
            set => UpdateObservable(ref _keycloakScope, value);
        }
        #endregion

        #region Commands
        public ICommand SaveSettingsCommand { get; }
        public ICommand ResetSettingsCommand { get; }
        #endregion

        public SettingsViewModel(ISettingsManager settings, ILogger<SettingsViewModel> logger)
        {
            _settings = settings;
            _logger = logger;
            _paletteHelper = new PaletteHelper();

            LoadSettings();

            SaveSettingsCommand = new DelegateCommand(_ => SaveSettings());
            ResetSettingsCommand = new DelegateCommand(_ => ResetSettings());
        }

        private void LoadSettings()
        {
            try
            {
                // Load Theme
                IsDark = GetSetting(nameof(IsDark), false);

                // Load Calculator Settings
                CountThreshold = GetSetting(nameof(CountThreshold), 10);

                // Load Auth Provider
                AuthProvider = GetSetting(nameof(AuthProvider), "msal");

                // Load MSAL Settings
                MsalClientId = GetSetting(nameof(MsalClientId), "");
                MsalTenantId = GetSetting(nameof(MsalTenantId), "common");
                MsalRedirectUri = GetSetting(nameof(MsalRedirectUri), "http://localhost");
                MsalScopes = GetSetting(nameof(MsalScopes), "User.Read");
                MsalUseBroker = GetSetting(nameof(MsalUseBroker), false);

                // Load Keycloak Settings
                KeycloakBaseUrl = GetSetting(nameof(KeycloakBaseUrl), "https://localhost:8080");
                KeycloakRealm = GetSetting(nameof(KeycloakRealm), "my_realm");
                KeycloakClientId = GetSetting(nameof(KeycloakClientId), "");
                KeycloakClientSecret = GetSetting(nameof(KeycloakClientSecret), "");
                KeycloakRedirectUri = GetSetting(nameof(KeycloakRedirectUri), "http://127.0.0.1:7890/callback");
                KeycloakScope = GetSetting(nameof(KeycloakScope), "openid profile");

                _logger?.LogInformation("Settings loaded successfully");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error loading settings");
            }
        }

        private void SaveSettings()
        {
            try
            {
                // Save Calculator Settings
                _settings.SetParameter(nameof(CountThreshold), CountThreshold);

                // Save Auth Provider
                _settings.SetParameter(nameof(AuthProvider), AuthProvider);

                // Save MSAL Settings
                _settings.SetParameter(nameof(MsalClientId), MsalClientId ?? "");
                _settings.SetParameter(nameof(MsalTenantId), MsalTenantId ?? "common");
                _settings.SetParameter(nameof(MsalRedirectUri), MsalRedirectUri ?? "http://localhost");
                _settings.SetParameter(nameof(MsalScopes), MsalScopes ?? "User.Read");
                _settings.SetParameter(nameof(MsalUseBroker), MsalUseBroker);

                // Save Keycloak Settings
                _settings.SetParameter(nameof(KeycloakBaseUrl), KeycloakBaseUrl ?? "https://localhost:8080");
                _settings.SetParameter(nameof(KeycloakRealm), KeycloakRealm ?? "my_realm");
                _settings.SetParameter(nameof(KeycloakClientId), KeycloakClientId ?? "");
                _settings.SetParameter(nameof(KeycloakClientSecret), KeycloakClientSecret ?? "");
                _settings.SetParameter(nameof(KeycloakRedirectUri), KeycloakRedirectUri ?? "http://127.0.0.1:7890/callback");
                _settings.SetParameter(nameof(KeycloakScope), KeycloakScope ?? "openid profile");

                _logger?.LogInformation("Settings saved successfully");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error saving settings");
            }
        }

        private void ResetSettings()
        {
            LoadSettings();
            _logger?.LogInformation("Settings reset to saved values");
        }

        private T GetSetting<T>(string key, T defaultValue)
        {
            try
            {
                var value = _settings.GetParameter(key);
                if (value == null)
                    return defaultValue;

                if (typeof(T) == typeof(bool))
                    return (T)(object)Convert.ToBoolean(value);
                if (typeof(T) == typeof(int))
                    return (T)(object)Convert.ToInt32(value);
                if (typeof(T) == typeof(string))
                    return (T)(object)(value?.ToString() ?? defaultValue?.ToString() ?? "");

                return (T)value;
            }
            catch
            {
                return defaultValue;
            }
        }

        private void SetTheme(bool isDark)
        {
            try
            {
                Theme theme = _paletteHelper.GetTheme();
                if (isDark)
                {
                    theme.SetPrimaryColor(Color.FromArgb(255, 103, 58, 183));
                    theme.SetBaseTheme(BaseTheme.Dark);
                }
                else
                {
                    theme.SetPrimaryColor(Color.FromArgb(255, 103, 58, 183));
                    theme.SetBaseTheme(BaseTheme.Light);
                }
                _paletteHelper.SetTheme(theme);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error setting theme");
            }
        }
    }
}