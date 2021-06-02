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
        public Response<ItemCategory> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<ItemCategory>> GetAllByCategory(int categoryId)
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
