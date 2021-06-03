using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.DAL.EFRepositories
{
    public class EFCartItemRepository : ICartItemRepository
    {
        private OkeaFurnitureContext context;

        public EFCartItemRepository(OkeaFurnitureContext context)
        {
            this.context = context;
        }
        public Response<CartItem> Add(int cartId, int itemId)
        {
            throw new NotImplementedException();
        }

        public Response Delete(int cartId, int itemId)
        {
            throw new NotImplementedException();
        }

        public Response<CartItem> Get(int cartId, int itemId)
        {
            throw new NotImplementedException();
        }

        public Response<List<CartItem>> GetByCart(int cartId)
        {
            throw new NotImplementedException();
        }

        public Response Update(int cartId, int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
