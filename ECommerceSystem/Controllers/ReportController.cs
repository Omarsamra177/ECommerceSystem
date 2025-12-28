using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.Core.Services;
using ECommerceSystem.DTOs.Reports;

namespace ECommerceSystem.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("sales")]
        public async Task<IActionResult> GetSalesReport()
        {
            var data = await _service.GetSalesReportAsync();

            var result = data.Select(d => new SalesReportDto
            {
                Date = d.Date,
                TotalSales = d.TotalSales
            });

            return Ok(result);
        }
    }
}
