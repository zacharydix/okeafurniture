using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public static class Mapper
    {
        public static Category MapToCategory(this CategoryModel model)
        {
            var items = new List<Item>();
            foreach (var itemModel in model.ItemModels) 
            {

            }
            return new Category()
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
                Items = model.ItemModels;
            };
        }

        public static CategoryModel MapToModel(this Category category)
        {
            return new CategoryModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        public static List<CategoryModel> MapToModel(this List<Category> categories)
        {
            var categoryModels = new List<CategoryModel>();

            foreach (var c in categories)
            {
                categoryModels.Add(
                    new CategoryModel()
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName
                    });
            };
            return categoryModels;
        }
    }
}
