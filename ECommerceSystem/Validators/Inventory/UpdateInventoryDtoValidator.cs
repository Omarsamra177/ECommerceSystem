using FluentValidation;
using ECommerceSystem.DTOs.Inventory;

namespace ECommerceSystem.API.Validators.Inventory
{
    public class UpdateInventoryDtoValidator : AbstractValidator<UpdateInventoryDto>
    {
        public UpdateInventoryDtoValidator()
        {
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock cannot be negative");
        }
    }
}
