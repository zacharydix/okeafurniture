using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public class CartModel
    {
        public int CartId { get; set; }

        [Required(ErrorMessage = "Account Id is required")]
        public int AccountId { get; set; }
      
        public int? PaymentMethodId { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime? CheckOutDate { get; set; }


        public List<CartItem> CartItems { get; set; }
        public Account Account { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        // further validation: if cart has checkOutDate, paymentMethod should be populated.
        // should order total be >=0?
    }
}
