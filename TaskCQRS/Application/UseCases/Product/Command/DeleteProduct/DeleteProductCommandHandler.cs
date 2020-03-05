using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Infrastructure.Persistences;
using TaskCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace TaskCQRS.Application.UseCases.Product.Command.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductCommandDto>
    {
        private readonly EcommerceContext _context;

        public DeleteProductCommandHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<DeleteProductCommandDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var delete = await _context.PaymentsData.FindAsync(request.Id);

            if (delete == null)
            {
                return new DeleteProductCommandDto
                {
                    Success = false,
                    Message = "Not Found"
                };
            }

            else
            {
                _context.PaymentsData.Remove(delete);
                await _context.SaveChangesAsync(cancellationToken);

                return new DeleteProductCommandDto
                {
                    Success = true,
                    Message = "Successfully retrieved customer"
                };

            }

        }
    }
}
