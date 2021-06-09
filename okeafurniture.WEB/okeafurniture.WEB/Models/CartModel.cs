using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public class CartModel : IValidatableObject
    {
        public int CartId { get; set; }

        [Required(ErrorMessage = "Account Id is required")]
        public int AccountId { get; set; }
      
        public int? PaymentMethodId { get; set; }
        
        [PositiveTotal]
        public decimal OrderTotal { get; set; }
        public DateTime? CheckOutDate { get; set; }


        public List<CartItem> CartItems { get; set; }
        public Account Account { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (CheckOutDate.HasValue && !PaymentMethodId.HasValue)
            {
                errors.Add(new ValidationResult("Cart must have payment method ID at check out", new string[] { "PaymentMethodId" }));
            }

            return errors;
        }
    }
}
