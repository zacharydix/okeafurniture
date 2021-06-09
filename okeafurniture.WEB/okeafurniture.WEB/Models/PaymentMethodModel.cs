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

        [Required(ErrorMessage = "Card holder first name is required")]
        public string CardHolderFirstName { get; set; }

        [Required(ErrorMessage = "Card holder last name is required")]
        public string CardHolderLastName { get; set; }

        [Required(ErrorMessage = "Card number is required")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Card expiration is required")]
        public DateTime CardExpiration { get; set; }

        [Required(ErrorMessage = "CVV is required")]
        public string CardCVV { get; set; }

        [Required(ErrorMessage = "Billing address is required")]
        public string BillingAddress { get; set; }
        public Account Account { get; set; }
    }
}
