using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.Core.Services;
using ECommerceSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSystem.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<(DateTime Date, decimal TotalSales)>> GetSalesReportAsync()
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .ToListAsync();

            return orders
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => (
                    g.Key,
                    g.Sum(o => o.TotalAmount)
                ))
                .OrderBy(r => r.Key)
                .ToList();
        }
    }
}
