using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.DTOs
{
    public class TopItemListItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsSold { get; set; }
        public decimal Revenue { get; set; }
    }
}
