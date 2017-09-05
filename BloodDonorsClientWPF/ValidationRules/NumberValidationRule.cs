using System.Globalization;
using System.Windows.Controls;

namespace BloodDonorsClientWPF.ValidationRules
{
    public class NumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var result = int.TryParse(value as string, out int temp);

            return result ? ValidationResult.ValidResult : new ValidationResult(false, "Must be a number");
        }
    }
}