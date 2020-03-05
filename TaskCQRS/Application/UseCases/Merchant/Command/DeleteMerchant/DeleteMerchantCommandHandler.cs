using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Infrastructure.Persistences;
using TaskCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace TaskCQRS.Application.UseCases.Merchant.Command.DeleteMerchant
{
    public class DeleteMerchantCommandHandler : IRequestHandler<DeleteMerchantCommand, DeleteMerchantCommandDto>
    {
        private readonly EcommerceContext _context;

        public DeleteMerchantCommandHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<DeleteMerchantCommandDto> Handle(DeleteMerchantCommand request, CancellationToken cancellationToken)
        {
            var delete = await _context.PaymentsData.FindAsync(request.Id);

            if (delete == null)
            {
                return new DeleteMerchantCommandDto
                {
                    Success = false,
                    Message = "Not Found"
                };
            }

            else
            {
                _context.PaymentsData.Remove(delete);
                await _context.SaveChangesAsync(cancellationToken);

                return new DeleteMerchantCommandDto
                {
                    Success = true,
                    Message = "Successfully retrieved customer"
                };

            }

        }
    }
}
