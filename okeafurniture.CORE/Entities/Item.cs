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
        public string ItemDescription { get; set; }
        public decimal UnitPrice {get;set;}

        public List<Category> Categories { get;set; }

        public Item()
        {
            Categories = new List<Category>();
        }

    }
}
