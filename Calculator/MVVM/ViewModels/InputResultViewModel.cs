using MVVM;

namespace Calculator.MVVM.ViewModels
{
    public class InputResultViewModel:BaseViewModel
    {
        private string _inputText = "0";
        public string InputText
        {
            get => _inputText;
            set => UpdateObservable(ref _inputText, value);
        }
        private string _outputText = "0";
        public string OutputText
        {
            get => _outputText;
            set => UpdateObservable(ref _outputText, value);
        }
        public InputResultViewModel()
        {
            
        }
    }
}
