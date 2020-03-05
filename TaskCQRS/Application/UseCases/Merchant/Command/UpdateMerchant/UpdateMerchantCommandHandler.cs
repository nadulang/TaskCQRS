using System;
using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Infrastructure.Persistences;
using TaskCQRS.Application.Models.Query;
using TaskCQRS.Domain.Entities;
using MediatR;


namespace TaskCQRS.Application.UseCases.Merchant.Command.UpdateMerchant
{
    public class UpdateMerchantCommandHandler : IRequestHandler<UpdateMerchantCommand, UpdateMerchantCommandDto>
    {
        private readonly EcommerceContext _context;
        public UpdateMerchantCommandHandler(EcommerceContext context)
        {
            _context = context;
        }
        public async Task<UpdateMerchantCommandDto> Handle(UpdateMerchantCommand request, CancellationToken cancellationToken)
        {
            var merch = _context.MerchantsData.Find(request.Data.id);

            merch.name = request.Data.name;
            merch.image = request.Data.image;
            merch.address = request.Data.address;
            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            merch.updated_at = (long)time;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateMerchantCommandDto
            {
                Success = true,
                Message = "Customer successfully updated",
            };
        }
    }
    
    
}
