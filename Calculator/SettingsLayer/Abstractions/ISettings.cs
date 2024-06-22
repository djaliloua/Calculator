namespace Calculator.SettingsLayer.Abstractions
{
    public interface ISettings
    {
        void SetParameter(string name, object value);
        object GetParameter(string name);
    }
}
