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
        public int UserId { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public DateTime CardExpiration { get; set; }
        public string CardCVV { get; set; }
        public string BillingAddress { get; set; }
        public User User { get; set; }
    }
}
