﻿using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.Views;
using Calculator.SettingsLayer.Abstractions;
using Calculator.SettingsLayer.Implementations;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using MVVM;
using System.Windows.Controls;

namespace Calculator.MVVM.ViewModels
{

    public class MainViewModel:BaseViewModel
    {
        #region Private properties
        public static event Action BottomDrawerOpened;
        private ISettings Settings;
        private readonly ILogger<MainViewModel> logger;
        #endregion

        protected virtual void OnBottomDrawerOpened() => BottomDrawerOpened?.Invoke();

        private bool _canMinimize;
        public bool IsFullScreen
        {
            get => _canMinimize;
            set => UpdateObservable(ref _canMinimize, value, () =>
            {
                if (_canMinimize)
                {
                    UserControl = new FullScreenLayout();
                }
                else
                {
                    UserControl = new SmallScreenLayout();
                }
            });
        }
        private bool _isBottomDrawerOpen;
        public bool IsBottomDrawerOpen
        {
            get => _isBottomDrawerOpen;
            set => UpdateObservable(ref _isBottomDrawerOpen, value, () =>
            {
                if(value == false)
                {
                    OnBottomDrawerOpened();
                }
            });
        }
        private UserControl _userControl;
        public UserControl UserControl
        {
            get => _userControl;
            set => UpdateObservable(ref _userControl, value);
        }
        
        private bool _isDark;
        public bool IsDark
        {
            get => _isDark;
            set => UpdateObservable(ref _isDark, value, () =>
            {
                SetTheme(IsDark);
                Settings.SetParameter(nameof(IsDark), value);
            });
        }
        
        public MainViewModel(Repository repository, ILogger<MainViewModel> _log)
        {
            logger = _log;
            logger.LogInformation("MainViewModel started......");
            Settings = new ThemeSettings();
            IsDark = (bool)Settings.GetParameter(nameof(IsDark));
            
        }
        private void SetTheme(bool isDark)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(isDark ? BaseTheme.Dark : BaseTheme.Light);
            paletteHelper.SetTheme(theme);
        }
    }
}
