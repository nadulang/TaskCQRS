using MediatR;
using TaskCQRS.Domain.Entities;

namespace TaskCQRS.Application.UseCases.Customer.Command.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<DeleteCustomerCommandDto>
    {
        public int Id { get; set; }

        public DeleteCustomerCommand(int id)
        {
            Id = id;
        }
    }
}
