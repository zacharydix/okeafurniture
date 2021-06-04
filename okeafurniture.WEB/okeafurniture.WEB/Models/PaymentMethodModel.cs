using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public class PaymentMethodModel
    {
        public int PaymentMethodId { get; set; }
        public int AccountId { get; set; }
        public string CardHolderFirstName { get; set; }
        public string CardHolderLastName { get; set; }
        public string CardNumber { get; set; }
        public DateTime CardExpiration { get; set; }
        public string CardCVV { get; set; }
        public string BillingAddress { get; set; }
        public Account Account { get; set; }
    }
}
