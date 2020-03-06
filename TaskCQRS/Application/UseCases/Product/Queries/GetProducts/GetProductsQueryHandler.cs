using System;
using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Application.Models.Query;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskCQRS.Domain.Entities;
using TaskCQRS.Infrastructure.Persistences;

namespace TaskCQRS.Application.UseCases.Product.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsDto>
    {
        private readonly EcommerceContext _context;

        public GetProductsQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetProductsDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.ProductsData.ToListAsync();

            var result = data.Select(e => new Products
            {
                id = e.id,
                merchant_id = e.merchant_id,
                name = e.name,
                price = e.price,
                created_at = e.created_at,
                updated_at = e.updated_at
            });

            return new GetProductsDto
            {
                Message = "Success retrieving data",
                Success = true,
                Data = result.ToList()
            };
        }
    }
}
