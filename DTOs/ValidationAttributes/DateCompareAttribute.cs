using DTOs.Models;
using System.ComponentModel.DataAnnotations;

namespace DTOs.ValidationAttributes
{
    public class DateCompareAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (RequestDto)validationContext.ObjectInstance;
            if (model.From > model.To)
            {
                return new ValidationResult("FROM date should be before TO date");
            }

            return ValidationResult.Success;
        }
    }
}
