using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Entities
{
    public class CategoryItem
    {
        public int CategoryId { get; set; }
        public int ItemId { get; set; }


        public Category Category { get; set; }
        public Item Item { get; set; }

    }
}
