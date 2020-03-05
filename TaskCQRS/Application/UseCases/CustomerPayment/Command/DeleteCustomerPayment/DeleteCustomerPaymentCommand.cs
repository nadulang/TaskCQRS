using MediatR;

namespace TaskCQRS.Application.UseCases.CustomerPayment.Command.DeleteCustomerPayment
{
    public class DeleteCustomerPaymentCommand : IRequest<DeleteCustomerPaymentCommandDto>
    {
        public int Id { get; set; }

        public DeleteCustomerPaymentCommand(int id)
        {
            Id = id;
        }
    }
}
