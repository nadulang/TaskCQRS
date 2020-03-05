using MediatR;
using TaskCQRS.Domain.Entities;

namespace TaskCQRS.Application.UseCases.CustomerPayment.Command.CreateCustomerPayment
{
    public class CreateCustomerPaymentCommand : IRequest<CreateCustomerPaymentCommandDto>
    {
        public CustomerPayments Data { get; set; }
    }
}
