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
        public Response<PaymentMethod> Add(PaymentMethod paymentMethod)
        {
            throw new NotImplementedException();
        }

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response<PaymentMethod> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<PaymentMethod>> GetByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Response Update(PaymentMethod paymentMethod)
        {
            throw new NotImplementedException();
        }
    }
}
