using System;
using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Application.Models.Query;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskCQRS.Domain.Entities;
using TaskCQRS.Infrastructure.Persistences;

namespace TaskCQRS.Application.UseCases.Customer.Queries.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, GetCustomersDto>
    {
        private readonly EcommerceContext _context;

        public GetCustomersQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetCustomersDto> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.CustomersData.ToListAsync();

            var result = data.Select(e => new Customers
            {
                id = e.id,
                full_name = e.full_name,
                username = e.username,
                birthdate = e.birthdate,
                password = e.password,
                gender = e.gender,
                email = e.email,
                phone_number = e.phone_number,
                created_at = e.created_at,
                updated_at = e.updated_at
                
            });

            return new GetCustomersDto
            {
                Message = "Success retrieving data",
                Success = true,
                Data = result.ToList()
            };
        }
    }
}