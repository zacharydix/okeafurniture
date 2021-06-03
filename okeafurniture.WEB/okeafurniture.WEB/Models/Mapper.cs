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
            return new Category()
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
                Items = model.Items
            };
        }

        public static CategoryModel MapToModel(this Category category)
        {
            return new CategoryModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Items = category.Items
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
                        CategoryName = c.CategoryName,
                        Items = c.Items
                    });
            };
            return categoryModels;
        }
    }
}
