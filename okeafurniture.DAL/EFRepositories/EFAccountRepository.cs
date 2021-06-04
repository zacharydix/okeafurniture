using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace okeafurniture.DAL
{
    public class EFAccountRepository : IAccountRepository
    {
        OkeaFurnitureContext context;
        public EFAccountRepository(OkeaFurnitureContext context)
        {
            this.context = context;
        }
        public Response<Account> Add(Account account)
        {
            Response<Account> response = new Response<Account>()
            {
                Data = null,
                Success = true,
                Message = "Successfully added account."
            };
            try
            {
                context.Accounts.Add(account);
                context.SaveChanges();
                response.Data = account;
                return response;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "An error occured when accessing data. Please contact support.";
                return response;
            }
        }

        public Response Delete(int id)
        {
            Response response = new Response()
            {
                Success = true,
                Message = "Successfully deleted account."
            };
            try
            {
                context.Accounts.Remove(context.Accounts.Find(id));
                foreach (var item in context.PaymentMethods.Where(pm => pm.AccountId == id))
                {
                    context.PaymentMethods.Remove(item);
                }
                foreach (var item in context.Carts.Where(c => c.AccountId == id))
                {
                    foreach (var item2 in context.CartItems.Where(ci => ci.CartId == item.CartId))
                    {
                        context.CartItems.Remove(item2);
                    }
                    context.Carts.Remove(item);
                }
                context.SaveChanges();
                return response;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "An error occured when accessing data. Please contact support.";
                return response;
            }
        }

        public Response<Account> Get(int id)
        {
            Response<Account> response = new Response<Account>()
            {
                Data = null,
                Success = true,
                Message = "Successfully retrieved account."
            };
            try
            {
                response.Data = context.Accounts.Find(id);
                if (response.Data == null)
                {
                    throw new KeyNotFoundException();
                }
                return response;
            }
            catch (Exception)
            {
                response.Data = null;
                response.Success = false;
                response.Message = "An error occured when accessing data. Please contact support.";
                return response;
            }
        }

        public Response<List<Account>> GetAll()
        {
            Response<List<Account>> response = new Response<List<Account>>()
            {
                Data = null,
                Success = true,
                Message = "Successfully retrieved accounts."
            };
            try
            {
                response.Data = context.Accounts.ToList();
                return response;
            }
            catch (Exception)
            {
                response.Data = null;
                response.Success = false;
                response.Message = "An error occured when accessing data. Please contact support.";
                return response;
            }
        }

        public Response<Account> GetByEmail(string email)
        {
            Response<Account> response = new Response<Account>()
            {
                Data = null,
                Success = true,
                Message = "Successfully retrieved account."
            };
            try
            {
                response.Data = context.Accounts.SingleOrDefault(a => a.Email == email);
                if (response.Data == null)
                {
                    throw new KeyNotFoundException();
                }
                return response;
            }
            catch (Exception)
            {
                response.Data = null;
                response.Success = false;
                response.Message = "An error occured when accessing data. Please contact support.";
                return response;
            }
        }

        public Response Update(Account account)
        {
            Response response = new Response()
            {
                Success = true,
                Message = "Successfully updated account."
            };
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    context.Accounts.Update(account);
                    context.SaveChanges();
                }
                return response;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "An error occured when accessing data. Please contact support.";
                return response;
            }

        }
    }
}
