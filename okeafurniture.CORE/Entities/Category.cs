using okeafurniture.CORE.Entities;
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
        public string ImageName { get; set; }
        public List<CategoryItem> CategoryItems { get; set; }

        public Category()
        {
            CategoryItems = new List<CategoryItem>();
        }
    }
}
