﻿using HelperClass;
using MVVM;
using System.Data;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels
{
    public class KeyBoardViewModel:BaseViewModel
    {
        private string specialCharackers = "*,.+/*-%";
        public ICommand AddInputCommand { get; private set; }
        public ICommand ResultCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public KeyBoardViewModel()
        {
            AddInputCommand = new DelegateCommand(addinput);
            ResultCommand = new DelegateCommand(on_result);
            DeleteAllCommand = new DelegateCommand(on_delete_all); 
            DeleteCommand = new DelegateCommand(on_delete);
        }
        private void on_delete(object parameter)
        {
            InputResultViewModel input = ServiceHelper.InputResultViewModel;
            if (input.InputText.Length > 1)
            {
                input.InputText = input.InputText[..(input.InputText.Length - 1)];
            }
            else
                input.InputText = "0";
        }
        private void on_delete_all(object parameter)
        {
            InputResultViewModel input = ServiceHelper.InputResultViewModel;
            input.InputText = "0";
            input.OutputText = "0";
        }
        private void on_result(object parameter)
        {
            InputResultViewModel input = ServiceHelper.InputResultViewModel;
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
            input.OutputText = Utility.RemoveComma(input.OutputText);
            input.OutputText = Utility.RoundOutput(input.OutputText);
            input.OutputText = Utility.RemoveComma(input.OutputText);

        }
        private void addinput(object parameter)
        {
            string p = parameter as string;
            if (!string.IsNullOrEmpty(p))
            {
                InputResultViewModel input = ServiceHelper.InputResultViewModel;
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
