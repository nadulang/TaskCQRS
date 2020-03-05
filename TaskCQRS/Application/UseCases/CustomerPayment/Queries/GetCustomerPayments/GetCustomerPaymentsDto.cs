using TaskCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using TaskCQRS.Application.Models.Query;

namespace TaskCQRS.Application.UseCases.CustomerPayment.Queries.GetCustomerPayments
{
    public class GetCustomerPaymentsDto : BaseDto
    {
        public IList<CustomerPayments> Data { get; set; }
    }
}
