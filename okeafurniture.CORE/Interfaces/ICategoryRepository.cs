using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Interfaces
{
    public interface ICategoryRepository
    {
        public Response<Category> Get(int id);
        public Response<List<Category>> GetAll();
        public Response<Category> Add(Category category);
		public Response Update(Category category);
		public Response Delete(int id);

    }
}
