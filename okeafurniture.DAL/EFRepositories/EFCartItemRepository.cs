using Microsoft.EntityFrameworkCore;
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
        public Response<CartItem> Add(CartItem cartItem)
        {
            Response<CartItem> response = new Response<CartItem>();
            try
            {
                context.CartItem.Add(cartItem);
                context.SaveChanges();

                //response.Data = cartItem;
                response.Data = context.CartItem
                    .Include(ci => ci.Item)
                    .SingleOrDefault(ci => (ci.CartId == cartItem.CartId) && (ci.ItemId == cartItem.ItemId));
                response.Success = true;
                response.Message = $"Successfully added new cart-item relationship {cartItem.CartId}-{cartItem.ItemId}";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response Delete(int cartId, int itemId)
        {
            Response response = new Response();
            try
            {

                var getResponse = Get(cartId, itemId);
                if (getResponse.Success)
                {
                    using (context = new OkeaFurnitureContext(context.Options))
                    {
                        context.CartItem.Remove(getResponse.Data);
                        context.SaveChanges();

                        response.Success = true;
                        response.Message = $"Successfully deleted cart-item {cartId}-{itemId}";
                    }
                }
                else
                {
                    response.Message = $"Could not find cart-item {cartId}-{itemId}";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<CartItem> Get(int cartId, int itemId)
        {
            Response<CartItem> response = new Response<CartItem>();
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    response.Data = context.CartItem.SingleOrDefault(ci => ci.CartId == cartId && ci.ItemId == itemId);
                    if (response.Data != null)
                    {
                        response.Success = true;
                        response.Message = $"Successfully retrieved cart-item";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Card-item relationship not found";
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

        public Response<List<CartItem>> GetByCart(int cartId)
        {
            Response<List<CartItem>> response = new Response<List<CartItem>>();
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    //response.Data = context.CartItem.Where(ci => ci.CartId == cartId).ToList();
                    //foreach (var cartItem in response.Data)
                    //{
                    //    cartItem.Item = context.Item.Find(cartItem.ItemId);
                    //}
                    response.Data = context.CartItem
                        .Include(ci => ci.Item)
                        .Where(ci => ci.CartId == cartId)
                        .ToList();
                    if (response.Data != null)
                    {
                        response.Success = true;
                        response.Message = $"Successfully retrieved items in cart #{cartId}";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Please enter a valid cart ID";
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

        public Response Update(CartItem cartItem)
        {
            Response response = new Response();
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    context.CartItem.Update(cartItem);
                    context.SaveChanges();
                    response.Success = true;
                    response.Message = $"successfully updated Cart-Item {cartItem.CartId}_{cartItem.ItemId}";
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
