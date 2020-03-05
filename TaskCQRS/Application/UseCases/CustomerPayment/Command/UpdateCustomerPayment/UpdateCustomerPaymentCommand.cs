using TaskCQRS.Domain.Entities;
using MediatR;

namespace TaskCQRS.Application.UseCases.CustomerPayment.Command.UpdateCustomerPayment
{
    public class UpdateCustomerPaymentCommand : IRequest<UpdateCustomerPaymentCommandDto>
    {
        public CustomerPayments Data { get; set; }
    }
}

