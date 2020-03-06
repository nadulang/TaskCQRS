using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TaskCQRS.Infrastructure.Persistences;

namespace TaskCQRS.Application.UseCases.CustomerPayment.Queries.GetCustomerPayment
{
    public class GetCustomerPaymentQueryHandler : IRequestHandler<GetCustomerPaymentQuery, GetCustomerPaymentDto>
    {
        private readonly EcommerceContext _context;

        public GetCustomerPaymentQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetCustomerPaymentDto> Handle(GetCustomerPaymentQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.PaymentsData.FindAsync(request.id);
            return new GetCustomerPaymentDto
            {
                Success = true,
                Message = "Payment succesfully retrieved",
                Data = result
            };
        }
    }
    
        
    
}
