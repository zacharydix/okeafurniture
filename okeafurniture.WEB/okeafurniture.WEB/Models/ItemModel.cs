using System.Collections.Generic;

namespace okeafurniture.WEB.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public List<CategoryModel> CategoryModels { get; set; }
        //add CartItems
    }
}
