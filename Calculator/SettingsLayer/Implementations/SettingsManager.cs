using Calculator.SettingsLayer.Abstractions;

namespace Calculator.SettingsLayer.Implementations
{
    public class SettingsManager : ISettingsManager
    {
        public object GetParameter(string name)
        {
            return Properties.Settings.Default[name];
        }

        public void SetParameter(string name, object value)
        {
            Properties.Settings.Default[name] = value;
            Properties.Settings.Default.Save();
        }
    }
}
