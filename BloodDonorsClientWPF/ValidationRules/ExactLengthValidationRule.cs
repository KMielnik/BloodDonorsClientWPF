using System.Globalization;
using System.Windows.Controls;

namespace BloodDonorsClientWPF.ValidationRules
{
    public class ExactLengthValidationRule : ValidationRule
    {
        public int Length { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var content = value as string;
            if(content.Length != Length)
                return new ValidationResult(false,$"Must be {Length} characters long");
            return ValidationResult.ValidResult;
        }
    }
}