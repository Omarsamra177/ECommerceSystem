using FluentValidation;
using ECommerceSystem.DTOs.Reviews;

namespace ECommerceSystem.API.Validators.Reviews
{
    public class CreateReviewDtoValidator : AbstractValidator<CreateReviewDto>
    {
        public CreateReviewDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5);

            RuleFor(x => x.Comment)
                .NotEmpty()
                .MinimumLength(5);
        }
    }
}
