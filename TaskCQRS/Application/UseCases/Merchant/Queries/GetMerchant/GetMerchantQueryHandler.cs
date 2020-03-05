using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TaskCQRS.Infrastructure.Persistences;

namespace TaskCQRS.Application.UseCases.Merchant.Queries.GetMerchant
{
    public class GetMerchantQueryHandler : IRequestHandler<GetMerchantQuery, GetMerchantDto>
    {
        private readonly EcommerceContext _context;

        public GetMerchantQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetMerchantDto> Handle(GetMerchantQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.MerchantsData.FirstOrDefaultAsync(e => e.id == request.id);
            return new GetMerchantDto
            {
                Success = true,
                Message = "Customer succesfully retrieved",
                Data =
                {
                    name = result.name,
                    image = result.image,
                    address = result.address,
                    rating = result.rating,
                    created_at = result.created_at,
                    updated_at = result.updated_at
                }
            };
        }
    }
}
