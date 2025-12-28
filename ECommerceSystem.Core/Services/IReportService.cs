using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceSystem.Core.Services
{
    public interface IReportService
    {
        Task<IEnumerable<(DateTime Date, decimal TotalSales)>> GetSalesReportAsync();
    }
}
