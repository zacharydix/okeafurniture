using okeafurniture.CORE.Entites;
using System.Collections.Generic;

namespace okeafurniture.WEB.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public List<Category> Categories { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
