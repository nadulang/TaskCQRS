using FluentValidation;
using System;

namespace TaskCQRS.Application.UseCases.CustomerPayment.Command.CreateCustomerPayment
{
    public class CreateCustomerPaymentCommandValidation : AbstractValidator<CreateCustomerPaymentCommand>
    {
        public CreateCustomerPaymentCommandValidation()
        {
            RuleFor(x => x.Data.name_on_card).NotEmpty().WithMessage("name_on_card can't be empty");
            RuleFor(x => x.Data.name_on_card).MaximumLength(50).WithMessage("max name_on_card length is 50");
            RuleFor(x => x.Data.exp_month).NotEmpty().WithMessage("exp_month can't be empty");
            RuleFor(x => Convert.ToInt32(x.Data.exp_month)).ExclusiveBetween(0, 13).WithMessage("exp_month is between 1-12");
            RuleFor(x => x.Data.exp_year).NotEmpty().WithMessage("exp_year can't be empty");
            RuleFor(x => x.Data.credit_card_number).CreditCard().WithMessage("credit_card_number must be type of credit card number");
        }
    }
}
