using Calculator.DataAccessLayer;
using Microsoft.Extensions.Logging;
using MVVM;

namespace Calculator.MVVM.ViewModels
{
    
    public class MainViewModel:BaseViewModel
    {
        public static event Action BottomDrawerOpened;
        protected virtual void OnBottomDrawerOpened() => BottomDrawerOpened?.Invoke();
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
        private readonly ILogger<MainViewModel> logger;
        
        public MainViewModel(IRepository repository, ILogger<MainViewModel> _log)
        {
            logger = _log;
            logger.LogInformation("MainViewModel started......");
        } 
    }
}
