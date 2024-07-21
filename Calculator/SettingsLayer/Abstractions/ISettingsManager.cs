namespace Calculator.SettingsLayer.Abstractions
{
    public interface ISettingsManager
    {
        void SetParameter(string name, object value);
        object GetParameter(string name);
    }
}
