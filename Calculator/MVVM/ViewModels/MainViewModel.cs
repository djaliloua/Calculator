using Calculator.DataAccessLayer;
using Microsoft.Extensions.Logging;
using MVVM;

namespace Calculator.MVVM.ViewModels
{
    public record Person(string name, string surname);
    public class MainViewModel:Loadable<Person>
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
            _ = LoadItems();
        }

        public override async Task LoadItems()
        {
            await Task.Delay(1);

            SetItems(Init());
        }
        private static IList<Person> Init()
        {
            return new List<Person>() 
            { 
                new Person("Ali", "Abdou"),
                new Person("Ali", "Abdou"),
                new Person("Ali", "Abdou"),
                new Person("Ali", "Abdou"),
                new Person("Ali", "Abdou"),
            };
        }
    }
}
