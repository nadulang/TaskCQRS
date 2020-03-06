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
            var result = await _context.MerchantsData.FindAsync(request.id);
            if (result == null)
            {
                return null;
            }
            else
            {
                return new GetMerchantDto
                {
                    Success = true,
                    Message = "Merchant succesfully retrieved",
                    Data = result
                };
            }
        }
    }
}
