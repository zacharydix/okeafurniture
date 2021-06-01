using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Entites
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int? PaymentMethodId { get; set; }
        public decimal OrderTotal { get; set; }
        public bool CheckedOut { get; set; }


        public List<CartItem> CartItems { get; set; }
        public User User { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
