using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public class PaymentMethodModel
    {
        public int PaymentMethodId { get; set; }

        [Required(ErrorMessage = "Account Id is required")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Card holder first name is required"),
            MaxLength(50, ErrorMessage = "Cannot exceed 50 characters")]
        public string CardHolderFirstName { get; set; }

        [Required(ErrorMessage = "Card holder last name is required"),
            MaxLength(50, ErrorMessage = "Cannot exceed 50 characters")]
        public string CardHolderLastName { get; set; }

        [Required(ErrorMessage = "Card number is required"),
            MaxLength(16, ErrorMessage = "Cannot exceed 16 digits")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Card expiration is required"), CardExpiration]
        public DateTime CardExpiration { get; set; }

        [Required(ErrorMessage = "CVV is required"),
            MaxLength(4, ErrorMessage = "Cannot exceed 4 digits")]
        public string CardCVV { get; set; }

        [Required(ErrorMessage = "Billing address is required"),
            MaxLength(50, ErrorMessage = "Cannot exceed 50 characters")]
        public string BillingAddress { get; set; }
        public Account Account { get; set; }
    }
}
