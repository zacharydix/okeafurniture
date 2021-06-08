using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.DTOs
{
    public class TopCustomerListItem
    {
        public int AccountId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
