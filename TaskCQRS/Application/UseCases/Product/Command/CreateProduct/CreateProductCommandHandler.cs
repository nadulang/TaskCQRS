using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Infrastructure.Persistences;
using MediatR;
using System;

namespace TaskCQRS.Application.UseCases.Product.Command.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandDto>
    {
        private readonly EcommerceContext _context;

        public CreateProductCommandHandler(EcommerceContext context)
        {
            _context = context;
        }
        public async Task<CreateProductCommandDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.Entities.Products
            {
                merchant_id = request.Data.merchant_id,
                name = request.Data.name,
                price = request.Data.price
            };

            _context.ProductsData.Add(product);
            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            product.created_at = (long)time;
            product.updated_at = (long)time;
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateProductCommandDto
            {
                Success = true,
                Message = "Your data succesfully created"
            };
        }
    }
}
