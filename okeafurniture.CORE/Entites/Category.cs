using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Entites
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<ItemCategory> ItemCategories { get; set; }

        public Category()
        {
            ItemCategories = new List<ItemCategory>();
        }
    }
}
