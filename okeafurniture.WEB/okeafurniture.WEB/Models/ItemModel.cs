using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace okeafurniture.WEB.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Item name is required")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Item description is required")]
        public string ItemDescription { get; set; }

        [Required(ErrorMessage = "Unit price is required")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Image name is required")]
        public string ImageName { get; set; }
        public List<CategoryItem> CategoryItems { get; set; }
        public List<CartItem> CartItems { get; set; }

        // further validation: unit price > 0
    }
}
