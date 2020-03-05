using MediatR;
namespace TaskCQRS.Application.UseCases.Customer.Queries.GetCustomer
{
    public class GetCustomerQuery : IRequest<GetCustomerDto>
    {
        
       public int id { get; set; }

        public GetCustomerQuery(int Id)
        {
            id = Id;
        }
        
    }
}
