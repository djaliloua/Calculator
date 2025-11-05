using MVVM;
using System.IO.Packaging;

namespace Calculator.MVVM.ViewModels.Standard
{
    public class InputResultViewModel:BaseViewModel
    {
        private string _inputText;
        public string InputText
        {
            get => _inputText;
            set => UpdateObservable(ref _inputText, value);
        }
        private string _outputText;
        public string OutputText
        {
            get => _outputText;
            set => UpdateObservable(ref _outputText, value);
        }
        public InputResultViewModel()
        {
            Initialize();
        }
        public void SetOpValue(string pValue, string ouputValue)
        {
             InputText = pValue;
            OutputText = ouputValue;
        }
        public InputResultViewModel(string val)
        {
            Val = val;
            InputText = val;
        }

        public string Val = "0";
        private void Initialize()
        {
            InputText = "0";
            OutputText = "0";
        }
    }
}
