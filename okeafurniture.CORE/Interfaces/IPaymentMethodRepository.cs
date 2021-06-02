using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Interfaces
{
    public interface IPaymentMethodRepository
    {
        public Response<PaymentMethod> Get(int id);
        public Response<List<PaymentMethod>> GetByUser(int accountId);
        public Response<PaymentMethod> Add(PaymentMethod paymentMethod);
        public Response Update(PaymentMethod paymentMethod);
        public Response Delete(int id);

    }
}
