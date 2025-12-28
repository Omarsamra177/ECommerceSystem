using System;

namespace ECommerceSystem.Infrastructure.Payments
{
    public class PaymentContext
    {
        private readonly CreditCardPaymentStrategy _creditCard;
        private readonly PaypalPaymentStrategy _paypal;
        private IPaymentStrategy _strategy;

        public PaymentContext(
            CreditCardPaymentStrategy creditCard,
            PaypalPaymentStrategy paypal)
        {
            _creditCard = creditCard;
            _paypal = paypal;
        }

        public void SetStrategy(string method)
        {
            _strategy = method.ToLower() switch
            {
                "creditcard" => _creditCard,
                "paypal" => _paypal,
                _ => throw new Exception("Invalid payment method")
            };
        }

        public void Pay(decimal amount)
        {
            _strategy.Pay(amount);
        }
    }
}
