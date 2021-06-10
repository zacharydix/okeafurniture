using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public class CategoryItemModel
    {
        [Required(ErrorMessage = "Category Id is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Item Id is required")]
        public int ItemId { get; set; }
        public Category Category { get; set; }
        public Item Item { get; set; }
    }
}
