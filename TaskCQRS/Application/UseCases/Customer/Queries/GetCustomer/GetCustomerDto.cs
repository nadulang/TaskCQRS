using TaskCQRS.Domain.Entities;
using TaskCQRS.Application.Models.Query;

namespace TaskCQRS.Application.UseCases.Customer.Queries.GetCustomer
{
    public class GetCustomerDto : BaseDto
    {
        public Customers Data { get; set; }
    }
}
