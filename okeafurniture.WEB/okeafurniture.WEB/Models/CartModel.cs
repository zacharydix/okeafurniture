using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public class CartModel
    {
        public int CartId { get; set; }
        public int AccountId { get; set; }
        public int? PaymentMethodId { get; set; }
        public decimal OrderTotal { get; set; }
        public bool CheckedOut { get; set; }
        //add Account? PaymentMethod?, CartItems
    }
}
