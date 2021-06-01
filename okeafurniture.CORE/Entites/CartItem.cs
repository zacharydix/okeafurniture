using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Entites
{
    public class CartItem
    {
        public int CartId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }


        public Cart Cart { get; set; }
        public Item Item { get; set; }

    }
}
