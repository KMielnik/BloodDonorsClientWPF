using System.Globalization;
using System.Windows.Controls;

namespace BloodDonorsClientWPF.ValidationRules
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var content = value as string;
            if(string.IsNullOrWhiteSpace(content))
                return new ValidationResult(false,"Can't be empty");
            return ValidationResult.ValidResult;
        }
    }
}