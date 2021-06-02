using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Entites
{
    public class PaymentMethod
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
