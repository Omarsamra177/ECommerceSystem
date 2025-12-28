using FluentValidation;
using ECommerceSystem.DTOs.Orders;

namespace ECommerceSystem.API.Validators.Orders
{
    public class UpdateOrderStatusDtoValidator : AbstractValidator<UpdateOrderStatusDto>
    {
        public UpdateOrderStatusDtoValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum();
        }
    }
}
