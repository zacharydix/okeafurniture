using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;

namespace okeafurniture.DAL.EFRepositories
{
    public class EFCartRepository : ICartRepository
    {
        private OkeaFurnitureContext context;

        public EFCartRepository(OkeaFurnitureContext context)
        {
            this.context = context;
        }

        public Response<Cart> Add(Cart cart)
        {
            Response<Cart> response = new Response<Cart>();
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    response.Data = context.Cart.Add(cart).Entity;
                    context.SaveChanges();
                    if (response.Data != null)
                    {
                        response.Success = true;
                        response.Message = $"Successfully created new cart for Account #{cart.AccountId}";
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

        public Response<Cart> Get(int id)
        {
            Response<Cart> response = new Response<Cart>();
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    response.Data = context.Cart.SingleOrDefault(c => c.CartId == id);
                    if (response.Data != null)
                    {
                        response.Success = true;
                        response.Message = $"Successfully retrieved cart #{id}";
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

        public Response<Cart> GetActive(int accountId)
        {
            Response<Cart> response = new Response<Cart>();
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    response.Data = context.Cart.SingleOrDefault(c => c.AccountId == accountId && c.CheckOutDate == null);
                    if (response.Data != null)
                    {
                        response.Success = true;
                        response.Message = $"Successfully retrieved active cart for Account #{accountId}";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "No active cart found";
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

        public Response<List<Cart>> GetAll()
        {
            Response<List<Cart>> response = new Response<List<Cart>>();
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    response.Data = context.Cart.ToList();
                    if (response.Data != null)
                    {
                        response.Success = true;
                        response.Message = "Successfully retrieved all carts";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "no carts";
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

        public Response<List<Cart>> GetAllByAccount(int accountId)
        {
            Response<List<Cart>> response = new Response<List<Cart>>();
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    response.Data = context.Cart.Where(c => c.AccountId == accountId).ToList();
                    if (response.Data != null)
                    {
                        response.Success = true;
                        response.Message = $"Successfully retrieved all carts from Account {accountId}";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "No carts found";
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

        public Response<List<Cart>> GetAllByStatus(bool CheckedOut)
        {
            Response<List<Cart>> response = new Response<List<Cart>>();
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    if (CheckedOut == true)
                    {
                        response.Data = context.Cart.Where(c => c.CheckOutDate != null).ToList();
                    }
                    else
                    {
                        response.Data = context.Cart.Where(c => c.CheckOutDate == null).ToList();
                    }
                    if (response.Data != null)
                    {
                        response.Success = true;
                        if (CheckedOut)
                        {
                            response.Message = $"Successfully retrieved all checked out carts";
                        }
                        else
                        {
                            response.Message = $"Successfully retrieved all active carts";
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "No carts found";
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

        public Response Update(Cart cart)
        {
            Response response = new Response();
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    context.Cart.Update(cart);
                    context.SaveChanges();
                    response.Success = true;
                    response.Message = $"successfully updated Cart #{cart.CartId}";
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
