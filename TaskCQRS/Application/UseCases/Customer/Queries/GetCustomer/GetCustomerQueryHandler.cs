using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TaskCQRS.Infrastructure.Persistences;

namespace TaskCQRS.Application.UseCases.Customer.Queries.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, GetCustomerDto>
    {
        private readonly EcommerceContext _context;

        public GetCustomerQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetCustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.CustomersData.FirstOrDefaultAsync(e => e.id == request.id);
            return new GetCustomerDto
            {
                Success = true,
                Message = "Customer succesfully retrieved",
                Data =
                {
                    id = result.id,
                    full_name = result.full_name,
                    username = result.username,
                    birthdate = result.birthdate,
                    gender = result.gender,
                    password = result.password,
                    email = result.email,
                    created_at = result.created_at,
                    updated_at = result.updated_at

                }
            };
        }
    }
}
