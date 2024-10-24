using Patterns.Implementation;
using RegistrationApplication.DataAccessLayer.Implementations;
using RegistrationApplication.Extensions;
using RegistrationApplication.MVVM.Models;
using System.IO;

namespace RegistrationApplication.MVVM.ViewModels.CountryViewModels
{
    public class CountryViewModel : BaseViewModel, IClone<CountryViewModel>
    {
        public int CountryId { get; set; }
        private string _globaleName;
        public string GlobalName
        {
            get => _globaleName;
            set => UpdateObservable(ref _globaleName, value);
        }

        private string _iso2;
        public string ISO2
        {
            get => _iso2;
            set => UpdateObservable(ref _iso2, value);
        }

        private string _iso3;
        public string ISO3
        {
            get => _iso3; 
            set => UpdateObservable(ref _iso3, value);
        }
        private string _code;
        public string Code
        {
            get => _code;
            set => UpdateObservable(ref _code, value);  
        }
        public CountryViewModel Clone() => (CountryViewModel)MemberwiseClone();
    }
    public class CountriesViewModel:Loadable<CountryViewModel> 
    {
        public CountriesViewModel()
        {
            Init();
        }
        private void Init()
        {
            using var repo = new GenericRepository<Country>();
            var data = repo.GetAll();
            LoadItems(data.ToVM());
        }
    }

    public class CountryViewModelUI:BaseViewModel
    {
        CountriesViewModel _listOfCountries;
        public CountriesViewModel ListOfCountries
        {
            get => _listOfCountries;
            set => UpdateObservable(ref _listOfCountries, value);
        }
        public CountryViewModelUI()
        {
            ListOfCountries = new CountriesViewModel();
        }
        private void LoadInitialeDataToDb()
        {
            string path = @"C:\Users\djali\Downloads\countryNames.csv";
            int i = 0;
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values.Length > 0 && i > 0)
                    {
                        using var repo = new GenericRepository<Country>();
                        repo.Save(new Country() { GlobalName = values[0], ISO2 = values[1], ISO3 = values[2], Code = values[3] });
                    }
                    i++;
                }
            }
        }
    }
}
