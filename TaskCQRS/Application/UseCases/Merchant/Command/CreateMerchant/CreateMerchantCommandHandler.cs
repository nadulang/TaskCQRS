using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Infrastructure.Persistences;
using MediatR;
using System;

namespace TaskCQRS.Application.UseCases.Merchant.Command.CreateMerchant
{
    public class CreateMerchantCommandHandler : IRequestHandler<CreateMerchantCommand, CreateMerchantCommandDto>
    {
        private readonly EcommerceContext _context;

        public CreateMerchantCommandHandler(EcommerceContext context)
        {
            _context = context;
        }
        public async Task<CreateMerchantCommandDto> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
        {
            var merchant = new Domain.Entities.Merchants
            {
                name = request.Data.name,
                image = request.Data.image,
                address = request.Data.address,
                rating = request.Data.rating
            };

            _context.MerchantsData.Add(merchant);
            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            merchant.created_at = (long)time;
            merchant.updated_at = (long)time;
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateMerchantCommandDto
            {
                Success = true,
                Message = "Your data succesfully created"
            };
        }
    }
}
