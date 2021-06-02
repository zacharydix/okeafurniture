using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using okeafurniture.CORE.Entites;
using okeafurniture.CORE;

namespace okeafurniture.CORE.Interfaces
{
    public class IItemRepository
    {
        Response<Item> Insert(Item item);
        Response Update(Item item);
        Response Delete(int itemId);
        Response<Item> Get(int itemId);
        Response<List<Item>> GetByCategory(int categoryId);
    }
}
