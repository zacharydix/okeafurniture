using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.DAL.EFRepositories
{
    public class EFItemCategoryRepository : IItemCategoryRepository
    {
        OkeaFurnitureContext _context;

        public EFItemCategoryRepository(OkeaFurnitureContext context)
        {
            _context = context;
        }

        public Response<ItemCategory> Get(int itemId, int categoryId)
        {
            var response = new Response<ItemCategory>();

            var itemCategory = _context.ItemCategories
                .SingleOrDefault(i => i.ItemId == itemId && i.CategoryId == categoryId);

            if (itemCategory == null)
            {
                response.Success = false;
                response.Message = "ItemCategory not found. ";
            }
            else
            {
                response.Data = itemCategory;
                response.Success = true;
            }

            return response;
        }

        public Response<List<ItemCategory>> GetAllByCategory(int categoryId)
        {
            var response = new Response<List<ItemCategory>>();

            var itemCategories = _context.ItemCategories
                .Where(i => i.CategoryId == categoryId)
                .ToList();

            if (itemCategories == null || itemCategories.Count == 0)
            {
                response.Success = false;
                response.Message = "No ItemCategories found for that category. ";
            }
            else
            {
                response.Success = true;
                response.Data = itemCategories;
            }
            return response;
        }

        public Response<List<ItemCategory>> GetCategoriesByItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public Response<ItemCategory> Add(ItemCategory itemCategory)
        {
            throw new NotImplementedException();
        }

        public Response Update(ItemCategory itemCategory)
        {
            throw new NotImplementedException();
        }

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
