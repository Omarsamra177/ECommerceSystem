using System;

namespace ECommerceSystem.DTOs.Payments
{
    public class PayOrderDto
    {
        public Guid OrderId { get; set; }
        public string Method { get; set; }
    }
}
