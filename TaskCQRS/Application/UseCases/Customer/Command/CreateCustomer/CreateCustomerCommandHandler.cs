using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Infrastructure.Persistences;
using TaskCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;

namespace TaskCQRS.Application.UseCases.Customer.Command.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandDto>
    {
        private readonly EcommerceContext _context;

        public CreateCustomerCommandHandler(EcommerceContext context)
        {
            _context = context; 
        }
        public async Task<CreateCustomerCommandDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Domain.Entities.Customers
            {
                full_name = request.Data.full_name,
                username = request.Data.username,
                gender = request.Data.gender,
                birthdate = request.Data.birthdate,
                password = request.Data.password,
                email = request.Data.email,
                phone_number = request.Data.phone_number
            };

            _context.CustomersData.Add(customer);
            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            customer.created_at = (long)time;
            customer.updated_at = (long)time;
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateCustomerCommandDto
            {
                Success = true,
                Message = "Your data succesfully created"
            };
        }
    }
}
