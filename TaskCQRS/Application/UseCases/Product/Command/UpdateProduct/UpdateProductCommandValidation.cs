using FluentValidation;
using System;

namespace TaskCQRS.Application.UseCases.Product.Command.UpdateProduct
{
    public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidation()
        {
            RuleFor(x => x.Data.name).NotEmpty().WithMessage("name can't be empty");
            RuleFor(x => x.Data.name).MaximumLength(50).WithMessage("max username length is 50");
            RuleFor(x => x.Data.price).NotEmpty().WithMessage("price can't be empty");
            RuleFor(x => x.Data.price).GreaterThan(1000).WithMessage("price must be greater than 1000");
        }
    }
}
