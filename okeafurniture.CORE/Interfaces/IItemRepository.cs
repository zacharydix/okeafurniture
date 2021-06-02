using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Interfaces
{
    public interface IItemRepository
    {
        public Response<Item> Insert(Item item);
        public Response Update(Item item);
        public Response Delete(int itemId);
        public Response<Item> Get(int itemId);
        public Response<List<Item>> GetByCategory(int categoryId);
    }
}
