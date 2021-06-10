using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public class DateOfBirthAttribute : ValidationAttribute
    {
        //6570 days ~ 18 yrs
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime)
            {
                DateTime date = Convert.ToDateTime(value);

                var eighteenYears = new TimeSpan(6570, 0, 0, 0);
                if (date >= DateTime.Today - eighteenYears)
                {
                    return new ValidationResult("You must be 18 years or older to register");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult("This attribute only works with DateTime objects");
            }

        }
    }
}
