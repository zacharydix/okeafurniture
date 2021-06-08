using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.DTOs
{
    public class RevenueSummaryListItem
    {
        public DateTime SaleDate { get; set; }
        public int QuantitySold { get; set; }
        public decimal RevenueGenerated { get; set; }
    }
}
