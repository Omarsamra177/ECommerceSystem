using System;
using System.Threading.Tasks;

namespace ECommerceSystem.Core.Services
{
    public interface IPaymentService
    {
        Task PayAsync(Guid orderId, string method);
    }
}
