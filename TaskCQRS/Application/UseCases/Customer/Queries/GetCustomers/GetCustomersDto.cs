using TaskCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using TaskCQRS.Application.Models.Query;
using MediatR;

namespace TaskCQRS.Application.UseCases.Customer.Queries.GetCustomers
{
    public class GetCustomersDto : BaseDto
    {
        public IList<Customers> Data { get; set; }
    }
}
