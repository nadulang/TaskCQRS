using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TaskCQRS.Infrastructure.Persistences;

namespace TaskCQRS.Application.UseCases.Product.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductDto>
    {
        private readonly EcommerceContext _context;

        public GetProductQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.ProductsData.FindAsync(request.Id);
            return new GetProductDto
            {
                Success = true,
                Message = "Product succesfully retrieved",
                Data = result
            };
        }
    }
}
