using System;
using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Application.Models.Query;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskCQRS.Domain.Entities;
using TaskCQRS.Infrastructure.Persistences;

namespace TaskCQRS.Application.UseCases.CustomerPayment.Queries.GetCustomerPayments
{
    public class GetCustomerPaymentsQueryHandler : IRequestHandler<GetCustomerPaymentsQuery, GetCustomerPaymentsDto>
    {
        private readonly EcommerceContext _context;

        public GetCustomerPaymentsQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetCustomerPaymentsDto> Handle(GetCustomerPaymentsQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.PaymentsData.ToListAsync();

            var result = data.Select(e => new CustomerPayments
            {
                customer_id = e.customer_id,
                name_on_card = e.name_on_card,
                exp_month = e.exp_month,
                exp_year = e.exp_year,
                postal_code = e.postal_code,
                credit_card_number = e.credit_card_number,
                created_at = e.created_at,
                updated_at = e.updated_at
            });

            return new GetCustomerPaymentsDto
            {
                Message = "Success retrieving data",
                Success = true,
                Data = result.ToList()
            };
        }
    }
}
