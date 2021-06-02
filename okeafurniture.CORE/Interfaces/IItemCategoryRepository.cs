using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Interfaces
{
    public interface IItemCategoryRepository
    {
        public Response<ItemCategory> Get(int itemId, int categoryId);
        public Response<List<ItemCategory>> GetAllByCategory(int categoryId);
        public Response<List<ItemCategory>> GetCategoriesByItem(int itemId);
        public Response<ItemCategory> Add(ItemCategory itemCategory);
        public Response Update(ItemCategory itemCategory);
        public Response Delete(int id);
    }
}
