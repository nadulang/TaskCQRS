using System;
using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Infrastructure.Persistences;
using TaskCQRS.Application.Models.Query;
using TaskCQRS.Domain.Entities;
using MediatR;

namespace TaskCQRS.Application.UseCases.Customer.Command.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandDto>
    {
        private readonly EcommerceContext _context;
        public UpdateCustomerCommandHandler(EcommerceContext context)
        {
            _context = context;
        }
        public async Task<UpdateCustomerCommandDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customers = _context.CustomersData.Find(request.Data.id);

            customers.full_name = request.Data.full_name;
            customers.username = request.Data.username;
            customers.birthdate = request.Data.birthdate;
            customers.password = request.Data.password;
            customers.gender = request.Data.gender;
            customers.email = request.Data.email;
            customers.phone_number = request.Data.phone_number;
            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            customers.updated_at = (long)time;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateCustomerCommandDto
            {
                Success = true,
                Message = "Customer successfully updated",
            };
        }
    }
}
