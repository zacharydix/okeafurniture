using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Entites
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public int CategoryId { get; set; }

        public decimal UnitPrice {get;set;}

        public List<ItemCategory> ItemCategory { get;set; }

        public Item()
        {
            ItemCategory = new List<ItemCategory>();
        }

    }
}
