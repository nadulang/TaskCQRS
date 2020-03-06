using FluentValidation;
namespace TaskCQRS.Application.UseCases.Product.Queries.GetProduct
{
    public class GetProductValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty().WithMessage("Id harus terdaftar.");
        }
    }
}
