using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response<Account> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<Account>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Response Update(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
