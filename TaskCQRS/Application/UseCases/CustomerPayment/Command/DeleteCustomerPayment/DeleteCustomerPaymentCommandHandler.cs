using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Infrastructure.Persistences;
using TaskCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace TaskCQRS.Application.UseCases.CustomerPayment.Command.DeleteCustomerPayment
{
    public class DeleteCustomerPaymentCommandHandler : IRequestHandler<DeleteCustomerPaymentCommand, DeleteCustomerPaymentCommandDto>
    {
        private readonly EcommerceContext _context;

        public DeleteCustomerPaymentCommandHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<DeleteCustomerPaymentCommandDto> Handle(DeleteCustomerPaymentCommand request, CancellationToken cancellationToken)
        {
            var delete = await _context.PaymentsData.FindAsync(request.Id);

            if (delete == null)
            {
                return null;
            }

            else
            {
                _context.PaymentsData.Remove(delete);
                await _context.SaveChangesAsync(cancellationToken);

                return new DeleteCustomerPaymentCommandDto
                {
                    Success = true,
                    Message = "Successfully retrieved customer"
                };

            }

        }
    }
}
