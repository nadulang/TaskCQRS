using FluentValidation;

namespace TaskCQRS.Application.UseCases.Merchant.Queries.GetMerchant
{
    public class GetMerchantValidator : AbstractValidator<GetMerchantQuery>
    {
        public GetMerchantValidator()
        {
            RuleFor(x => x.id).GreaterThan(0).NotEmpty().WithMessage("Id harus terdaftar.");
        }
    }
}
