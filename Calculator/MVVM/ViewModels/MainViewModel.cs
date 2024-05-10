using Calculator.DataAccessLayer;
using Calculator.MVVM.Models;
using Microsoft.Extensions.Logging;
using MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        
        private bool _isBottomDrawerOpen;
        public bool IsBottomDrawerOpen
        {
            get => _isBottomDrawerOpen;
            set => UpdateObservable(ref _isBottomDrawerOpen, value);
        }
        private readonly ILogger<MainViewModel> logger;
        
        public MainViewModel(IRepository repository, ILogger<MainViewModel> _log)
        {
            logger = _log;
            logger.LogInformation("MainViewModel started......");
            
        }
        
    }
}
