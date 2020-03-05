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
            var result = await _context.PaymentsData.FirstOrDefaultAsync(e => e.id == request.id);
            return new GetCustomerPaymentDto
            {
                Success = true,
                Message = "Customer succesfully retrieved",
                Data =
                {
                    customer_id = result.customer_id,
                    name_on_card = result.name_on_card,
                    exp_month = result.exp_month,
                    exp_year = result.exp_year,
                    postal_code = result.postal_code,
                    credit_card_number = result.credit_card_number,
                    created_at = result.created_at,
                    updated_at = result.updated_at

                }
            };
        }
    }
    
        
    
}
