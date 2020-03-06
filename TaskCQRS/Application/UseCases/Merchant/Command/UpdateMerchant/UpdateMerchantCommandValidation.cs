using FluentValidation;
using System;

namespace TaskCQRS.Application.UseCases.Merchant.Command.UpdateMerchant
{
    public class UpdateMerchantCommandValidation : AbstractValidator<UpdateMerchantCommand>
    {
        public UpdateMerchantCommandValidation()
        {
            RuleFor(x => x.Data.name).NotEmpty().WithMessage("name can't be empty");
            RuleFor(x => x.Data.address).NotEmpty().WithMessage("address can't be empty");
            RuleFor(x => x.Data.rating).ExclusiveBetween(0, 6).WithMessage("rating is bettween 1-5");
        }
    }
}
