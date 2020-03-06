using FluentValidation;
using System;

namespace TaskCQRS.Application.UseCases.Customer.Command.UpdateCustomer
{
    public class UpdateCustomerCommandValidation : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidation()
        {
            RuleFor(x => x.Data.username).NotEmpty().WithMessage("username can't be empty");
            RuleFor(x => x.Data.username).MaximumLength(20).WithMessage("max username length is 20");
            RuleFor(x => x.Data.email).NotEmpty().WithMessage("email can't be empty");
            RuleFor(x => x.Data.email).EmailAddress().WithMessage("email is not valid email address");
            RuleFor(x => x.Data.gender).IsInEnum().WithMessage("gender is one of male or female");
            RuleFor(x => x.Data.gender).NotEmpty().WithMessage("gender can't be empty");
            RuleFor(x => x.Data.birthdate).NotEmpty().WithMessage("birthdate can't be empty");
            RuleFor(x => DateTime.Now.Year - x.Data.birthdate.Year).GreaterThanOrEqualTo(18).WithMessage("age must be greater than 18");
        }
    }
}
