using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public class CartItemModel
    {
        public int CartId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public Cart Cart { get; set; }
        public Item Item { get; set; }
    }
}
