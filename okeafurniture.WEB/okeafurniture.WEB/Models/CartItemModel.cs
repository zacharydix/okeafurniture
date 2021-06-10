using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public class CartItemModel
    {
        [Required(ErrorMessage ="Cart Id is required")]
        public int CartId { get; set; }

        [Required(ErrorMessage = "Item Id is required")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        public Cart Cart { get; set; }
        public Item Item { get; set; }
    }
}
