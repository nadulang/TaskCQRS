using FluentValidation;
namespace TaskCQRS.Application.UseCases.CustomerPayment.Queries.GetCustomerPayment
{
    public class GetCustomerPaymentValidator : AbstractValidator<GetCustomerPaymentQuery>
    {
        public GetCustomerPaymentValidator()
        {
            RuleFor(x => x.id).GreaterThan(0).NotEmpty().WithMessage("Id harus terdaftar.");
        }
    }
}
