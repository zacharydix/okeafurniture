using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using okeafurniture.CORE.Entites;
using okeafurniture.CORE;

namespace okeafurniture.CORE.Interfaces
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}
