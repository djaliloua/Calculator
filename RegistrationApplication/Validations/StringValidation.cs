using System.Globalization;
using System.Windows.Controls;

namespace RegistrationApplication.Validations
{
    public class StringValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string obj = value as string;
            if(string.IsNullOrWhiteSpace(obj) || obj.Length < 1)
            {
                return new ValidationResult(false, $"Field must not be null");
            }
            return ValidationResult.ValidResult;
        }
    }
}
