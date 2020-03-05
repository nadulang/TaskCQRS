using System;
using TaskCQRS.Application.Models.Query;
using TaskCQRS.Domain.Entities;
using MediatR;

namespace TaskCQRS.Application.UseCases.Customer.Command.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<UpdateCustomerCommandDto>
    {
        public Customers Data { get; set; }
    }
}
