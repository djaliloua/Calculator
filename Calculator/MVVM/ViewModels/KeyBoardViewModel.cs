using Calculator.DataAccessLayer;
using HelperClass;
using MVVM;
using System.Data;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels
{
    public class KeyBoardViewModel:BaseViewModel
    {
        private string specialCharackers = "*,.+/*-%";
        private IRepository repository;
        public ICommand AddInputCommand { get; private set; }
        public ICommand ResultCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public KeyBoardViewModel(IRepository repository)
        {
            this.repository = repository;
            AddInputCommand = new DelegateCommand(addinput);
            ResultCommand = new DelegateCommand(on_result);
            DeleteAllCommand = new DelegateCommand(on_delete_all); 
            DeleteCommand = new DelegateCommand(on_delete);
        }
        private void on_delete(object parameter)
        {
            InputResultViewModel input = ServiceLocator.InputResultViewModel;
            if (input.InputText.Length > 1)
            {
                input.InputText = input.InputText[..(input.InputText.Length - 1)];
            }
            else
            {
                input.InputText = "0";
                input.OutputText = input.InputText;
            }
        }
        private void on_delete_all(object parameter)
        {
            ServiceLocator.InputResultViewModel.InputText = "0";
            ServiceLocator.InputResultViewModel.OutputText = "0";
        }
        private async void on_result(object parameter)
        {
            InputResultViewModel input = ServiceLocator.InputResultViewModel;
            try
            {
                object value = new DataTable().Compute(input.InputText, null);
                if (value != null)
                {
                    input.OutputText = value.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                input.OutputText = Utility.RemoveComma(input.OutputText);
                input.OutputText = Utility.RoundOutput(input.OutputText);
                input.OutputText = Utility.RemoveComma(input.OutputText);
                if (ServiceLocator.BottomViewModel.IsPresent(input.InputText) && !string.IsNullOrEmpty(input.InputText))
                {
                    var op = await repository.SaveItem(new(input.InputText, input.OutputText));
                    ServiceLocator.BottomViewModel.AddItem(op);
                }
            }
            catch (FormatException fe)
            {
                input.InputText = fe.Message;
            }
            catch (Exception ex)
            {
                input.InputText = ex.Message;
            }

        }
        private void addinput(object parameter)
        {
            string p = parameter as string;
            if (!string.IsNullOrEmpty(p))
            {
                InputResultViewModel input = ServiceLocator.InputResultViewModel;
                if (input.InputText == "0" && !specialCharackers.Contains(p))
                {
                    input.InputText = p;
                    if(p == "00")
                        input.InputText = "0";
                }
                else
                {
                    if(p == "%" && input.InputText != "0")
                    {
                        if(Percentage.IsValideExpression(input.InputText))
                            input.InputText = Percentage.MakePercentage(input.InputText);
                    }
                    else if(input.InputText != "0")
                    {
                        input.InputText += p;
                    }
                }
                if (p == "." && input.InputText == "0")
                    input.InputText = "0.";

            }
        }
    }
}
