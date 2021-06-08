using okeafurniture.CORE.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Entites
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal UnitPrice {get;set;}
        public string ImageName { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<CategoryItem> CategoryItems { get; set; }


        public Item()
        {
            CategoryItems = new List<CategoryItem>();
            CartItems = new List<CartItem>();
        }
    }
}
