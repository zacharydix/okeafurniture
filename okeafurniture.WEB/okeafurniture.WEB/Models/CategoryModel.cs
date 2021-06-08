using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageName { get; set; }
        public List<CategoryItem> CategoryItems { get; set; }
    }
}
