using Calculator.DataAccessLayer.Implementations;
using HelperClass;
using MVVM;
using System.Data;
using System.Diagnostics;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels.Standard
{
    public class KeyBoardViewModel:BaseViewModel
    {
        #region Private properties
        private string specialCharackers = "*,.+/*-%";
        #endregion

        #region Commands
        public ICommand AddInputCommand { get; private set; }
        public ICommand ResultCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        #endregion

        #region Constructor
        public KeyBoardViewModel()
        {
            AddInputCommand = new DelegateCommand(AddInput);
            ResultCommand = new DelegateCommand(OnResult);
            DeleteAllCommand = new DelegateCommand(OnDeleteAll); 
            DeleteCommand = new DelegateCommand(OnDelete);
        }
        #endregion

        #region Handlers
        private void OnDelete(object parameter)
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
        private void OnDeleteAll(object parameter)
        {
            ServiceLocator.InputResultViewModel.InputText = "0";
            ServiceLocator.InputResultViewModel.OutputText = "0";
        }
        private void OnResult(object parameter)
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
                if (ServiceLocator.BottomViewModel.OperationVM.IsPresent(input.InputText) && !string.IsNullOrEmpty(input.InputText))
                {
                    ServiceLocator.BottomViewModel.AddOperation(input.InputText, input.OutputText);
                }
            }
            catch (FormatException fe)
            {
                input.InputText = "Error(Too long input)";
                Debug.WriteLine(fe.Message);
            }
            catch (Exception ex)
            {
                input.InputText = ex.Message;
            }

        }
        private void AddInput(object parameter)
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
                        char lastChar = input.InputText[input.InputText.Length - 1];
                        if (lastChar == '.' && p == ".")
                        {
                            return;
                        }
                        input.InputText += p;
                    }
                }
                if (p == "." && input.InputText == "0")
                    input.InputText = "0.";

            }
        }
        #endregion
    }
}
