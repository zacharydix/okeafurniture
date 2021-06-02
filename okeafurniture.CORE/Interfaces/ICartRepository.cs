using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Interfaces
{
    public interface ICartRepository
    {
        public Response<Cart> Get(int id);
        public Response<Cart> GetActive(int accountId);
        public Response<List<Cart>> GetAll();
        public Response<List<Cart>> GetAllByAccount(int accountId);
        public Response<List<Cart>> GetAllByStatus(bool CheckedOut);
        public Response<Cart> Add(int accountId);
        public Response Update(int accountId);
    }
}
