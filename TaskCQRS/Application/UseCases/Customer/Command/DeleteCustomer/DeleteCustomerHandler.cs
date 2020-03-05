using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Infrastructure.Persistences;
using TaskCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace TaskCQRS.Application.UseCases.Customer.Command.DeleteCustomer
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, DeleteCustomerCommandDto>
    {
        private readonly EcommerceContext _context;

        public DeleteCustomerHandler(EcommerceContext context)
        {
            _context = context;
        }
        public async Task<DeleteCustomerCommandDto> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var delete = await _context.CustomersData.FindAsync(request.Id);

            if (delete == null)
            {
                return new DeleteCustomerCommandDto
                {
                    Success = false,
                    Message = "Not Found"
                };
            }

            else
            { 
                _context.CustomersData.Remove(delete);
                await _context.SaveChangesAsync(cancellationToken);

                return new DeleteCustomerCommandDto
                {
                    Success = true,
                    Message = "Successfully retrieved customer"
                };

            }
           
        }
    }
}
