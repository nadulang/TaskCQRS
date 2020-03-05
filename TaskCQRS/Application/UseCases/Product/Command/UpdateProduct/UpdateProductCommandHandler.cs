using System;
using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Infrastructure.Persistences;
using TaskCQRS.Application.Models.Query;
using TaskCQRS.Domain.Entities;
using MediatR;

namespace TaskCQRS.Application.UseCases.Product.Command.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandDto>
    {
        private readonly EcommerceContext _context;
        public UpdateProductCommandHandler(EcommerceContext context)
        {
            _context = context;
        }
        public async Task<UpdateProductCommandDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _context.ProductsData.Find(request.Data.id);

            product.merchant_id = request.Data.merchant_id;
            product.name = request.Data.name;
            product.price = request.Data.price;
            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            product.updated_at = (long)time;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateProductCommandDto
            {
                Success = true,
                Message = "Customer successfully updated",
            };
        }
    }
}
