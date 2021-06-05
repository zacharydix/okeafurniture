using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Interfaces
{
    public interface ICartItemRepository
    {
        public Response<CartItem> Get(int cartId, int itemId);
        public Response<List<CartItem>> GetByCart(int cartId);
        public Response<CartItem> Add(CartItem cartItem);
        public Response Update(CartItem cartItem);
        public Response Delete(int cartId, int itemId);
    }
}
