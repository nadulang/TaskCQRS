using FluentValidation;
using TaskCQRS.Domain.Entities;

namespace TaskCQRS.Application.UseCases.Customer.Queries.GetCustomer
{
    public class GetCustomerValidator : AbstractValidator<GetCustomerQuery>
    {
        public GetCustomerValidator()
        {
            RuleFor(x => x.id).GreaterThan(0).NotEmpty().WithMessage("Id harus terdaftar.");
        }
    }
}
