using okeafurniture.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Interfaces
{
    public interface ICategoryItemRepository
    {
        public Response<CategoryItem> Get(int categoryId, int itemId);
        public Response<CategoryItem> Add(CategoryItem categoryItem);
        public Response Update(CategoryItem categoryItem);
        public Response Delete(int categoryId, int itemId);
    }
}
