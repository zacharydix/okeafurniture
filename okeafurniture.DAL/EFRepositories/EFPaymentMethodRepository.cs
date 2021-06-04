using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.DAL.EFRepositories
{
    public class EFPaymentMethodRepository : IPaymentMethodRepository
    {
        OkeaFurnitureContext context;

        public EFPaymentMethodRepository(OkeaFurnitureContext context)
        {
            this.context = context;
        }
        public Response<PaymentMethod> Add(PaymentMethod paymentMethod)
        {
            Response<PaymentMethod> response = new Response<PaymentMethod>()
            {
                Data = null,
                Success = true,
                Message = "Successfully added payment method."
            };
            try
            {
                context.PaymentMethod.Add(paymentMethod);
                context.SaveChanges();
                response.Data = paymentMethod;
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
                Message = "Successfully deleted payment method."
            };
            try
            {
                context.PaymentMethod.Remove(context.PaymentMethod.Find(id));
                foreach (var item in context.Cart.Where(c => c.PaymentMethodId == id))
                {
                    foreach (var item2 in context.CartItem.Where(ci => ci.CartId == item.CartId))
                    {
                        context.CartItem.Remove(item2);
                    }
                    context.Cart.Remove(item);
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

        public Response<PaymentMethod> Get(int id)
        {
            Response<PaymentMethod> response = new Response<PaymentMethod>()
            {
                Data = null,
                Success = true,
                Message = "Successfully retrieved payment method."
            };
            try
            {
                response.Data = context.PaymentMethod.Find(id);
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

        public Response<List<PaymentMethod>> GetByUser(int accountId)
        {
            Response<List<PaymentMethod>> response = new Response<List<PaymentMethod>>()
            {
                Data = null,
                Success = true,
                Message = "Successfully retrieved payment methods."
            };
            try
            {
                response.Data = context.PaymentMethod.Where(pm => pm.AccountId == accountId).ToList();
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

        public Response Update(PaymentMethod paymentMethod)
        {
            Response response = new Response()
            {
                Success = true,
                Message = "Successfully updated payment method."
            };
            try
            {
                using (context = new OkeaFurnitureContext(context.Options))
                {
                    context.PaymentMethod.Update(paymentMethod);
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
