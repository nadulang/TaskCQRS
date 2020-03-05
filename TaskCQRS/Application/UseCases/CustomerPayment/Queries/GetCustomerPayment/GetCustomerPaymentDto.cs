using TaskCQRS.Domain.Entities;
using TaskCQRS.Application.Models.Query;

namespace TaskCQRS.Application.UseCases.CustomerPayment.Queries.GetCustomerPayment
{
    public class GetCustomerPaymentDto : BaseDto
    {
        public CustomerPayments Data { get; set; }
    }
}
