using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Entities;
using System.Collections.Generic;

namespace okeafurniture.WEB.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImageName { get; set; }
        public List<CategoryItem> CategoryItems { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
