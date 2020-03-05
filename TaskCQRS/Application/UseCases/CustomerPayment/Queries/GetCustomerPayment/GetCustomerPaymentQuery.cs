using MediatR;
namespace TaskCQRS.Application.UseCases.CustomerPayment.Queries.GetCustomerPayment
{
    public class GetCustomerPaymentQuery : IRequest<GetCustomerPaymentDto>
    {
        public int id { get; set; }
    }
}
