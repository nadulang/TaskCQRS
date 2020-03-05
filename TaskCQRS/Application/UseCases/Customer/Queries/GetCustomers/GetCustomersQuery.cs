using System;
using MediatR;

namespace TaskCQRS.Application.UseCases.Customer.Queries.GetCustomers
{
    public class GetCustomersQuery : IRequest<GetCustomersDto>
    { 
    }
}
