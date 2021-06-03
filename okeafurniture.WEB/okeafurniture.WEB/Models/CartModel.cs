using okeafurniture.CORE.Entites;
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
        public Account Account { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
