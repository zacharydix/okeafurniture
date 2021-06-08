using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using okeafurniture.CORE.DTOs;

namespace okeafurniture.CORE.Interfaces
{
    public interface IReportRepository
    {
        Response<List<TopItemListItem>> GetTopItems();
        Response<List<TopCustomerListItem>> GetTopCustomers();
        Response<List<RevenueSummaryListItem>> GetRevenueReport(DateTime StartDate, DateTime EndDate);
    }
}
