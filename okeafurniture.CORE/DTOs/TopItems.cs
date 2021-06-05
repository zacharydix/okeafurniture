using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.DTOs
{
    public class TopItems
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
