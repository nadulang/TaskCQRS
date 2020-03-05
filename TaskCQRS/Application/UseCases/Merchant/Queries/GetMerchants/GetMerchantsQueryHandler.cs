using System;
using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Application.Models.Query;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskCQRS.Domain.Entities;
using TaskCQRS.Infrastructure.Persistences;

namespace TaskCQRS.Application.UseCases.Merchant.Queries.GetMerchants
{
    public class GetMerchantsQueryHandler : IRequestHandler<GetMerchantsQuery, GetMerchantsDto>
    {
        private readonly EcommerceContext _context;

        public GetMerchantsQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetMerchantsDto> Handle(GetMerchantsQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.MerchantsData.ToListAsync();

            var result = data.Select(e => new Merchants
            {
                name = e.name,
                image = e.image,
                address = e.address,
                rating = e.rating,
                created_at = e.created_at,
                updated_at = e.updated_at
            });

            return new GetMerchantsDto
            {
                Message = "Success retrieving data",
                Success = true,
                Data = result.ToList()
            };
        }
    }
}
