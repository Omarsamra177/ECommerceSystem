namespace ECommerceSystem.Infrastructure.Payments
{
    public interface IPaymentStrategy
    {
        void Pay(decimal amount);
    }
}
