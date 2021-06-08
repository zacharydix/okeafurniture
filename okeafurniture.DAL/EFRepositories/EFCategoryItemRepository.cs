using okeafurniture.CORE.Entities;
using okeafurniture.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.DAL.EFRepositories
{
    public class EFCategoryItemRepository : ICategoryItemRepository
    {
        private OkeaFurnitureContext context;

        public EFCategoryItemRepository(OkeaFurnitureContext context)
        {
            this.context = context;
        }

        public Response<CategoryItem> Add(CategoryItem categoryItem)
        {
            Response<CategoryItem> response = new Response<CategoryItem>();
            try
            {
                response.Data = context.Add(categoryItem).Entity;
                context.SaveChanges();
                response.Success = true;
                response.Message = $"Successfully added new category-item relationship {categoryItem.CategoryId}-{categoryItem.ItemId}";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response Delete(int categoryId, int itemId)
        {
            Response response = new Response();
            try
            {

                var getResponse = Get(categoryId, itemId);
                if (getResponse.Success)
                {
                    using (context = new OkeaFurnitureContext(context.Options))
                    {
                        context.CategoryItem.Remove(getResponse.Data);
                        context.SaveChanges();

                        response.Success = true;
                        response.Message = $"Successfully deleted category-item {categoryId}-{itemId}";
                    }
                }
                else
                {
                    response.Message = $"Could not find category-item {categoryId}-{itemId}";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<CategoryItem> Get(int categoryId, int itemId)
        {
            Response<CategoryItem> response = new Response<CategoryItem>();
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    response.Data = context.CategoryItem.SingleOrDefault(ci => ci.CategoryId == categoryId && ci.ItemId == itemId);
                    if (response.Data != null)
                    {
                        response.Success = true;
                        response.Message = $"Successfully retrieved category-item";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Category-item relationship not found";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response Update(CategoryItem categoryItem)
        {
            Response response = new Response();
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    context.CategoryItem.Update(categoryItem);
                    context.SaveChanges();
                    response.Success = true;
                    response.Message = $"successfully updated Category-Item {categoryItem.CategoryId}_{categoryItem.ItemId}";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
