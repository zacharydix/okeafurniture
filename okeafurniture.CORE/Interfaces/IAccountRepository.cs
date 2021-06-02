using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Interfaces
{
    public interface IAccountRepository
    {
        public Response<Account> Get(int id);
        public Response<List<Account>> GetAll();
        public Response<Account> Add(Account account);
        public Response Update(Account account);
        public Response Delete(int id);
    }
}
