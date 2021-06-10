using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace okeafurniture.WEB.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Item name is required"),
            MaxLength(50, ErrorMessage = "Cannot exceed 50 characters")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Item description is required"),
            MaxLength(100, ErrorMessage = "Cannot exceed 100 characters")]
        public string ItemDescription { get; set; }

        [Required(ErrorMessage = "Unit price is required"), PositiveTotal]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Image name is required"),
            MaxLength(100, ErrorMessage = "Cannot exceed 100 characters")]
        public string ImageName { get; set; }
        public List<CategoryItem> CategoryItems { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
