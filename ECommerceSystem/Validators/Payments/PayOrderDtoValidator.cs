using FluentValidation;
using ECommerceSystem.DTOs.Payments;

namespace ECommerceSystem.API.Validators.Payments
{
    public class PayOrderDtoValidator : AbstractValidator<PayOrderDto>
    {
        public PayOrderDtoValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty();

            RuleFor(x => x.Method)
                .NotEmpty()
                .Must(m => m == "creditcard" || m == "paypal");
        }
    }
}
