using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required"),
            MaxLength(25, ErrorMessage = "Cannot exceed 25 characters")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Image name is required"),
            MaxLength(100, ErrorMessage = "Cannot exceed 100 characters")]
        public string ImageName { get; set; }
        public List<CategoryItem> CategoryItems { get; set; }
    }
}
