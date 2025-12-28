using System;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;
using ECommerceSystem.Core.Services;
using ECommerceSystem.Infrastructure.Data;
using ECommerceSystem.Infrastructure.Payments;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSystem.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly PaymentContext _paymentContext;

        public PaymentService(AppDbContext context, PaymentContext paymentContext)
        {
            _context = context;
            _paymentContext = paymentContext;
        }

        public async Task PayAsync(Guid orderId, string method)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
                throw new Exception("Order not found");

            if (order.Status != OrderStatus.Pending)
                throw new Exception("Order already paid");

            _paymentContext.SetStrategy(method);
            _paymentContext.Pay(order.TotalAmount);

            order.Status = OrderStatus.Paid;
            await _context.SaveChangesAsync();
        }
    }
}
