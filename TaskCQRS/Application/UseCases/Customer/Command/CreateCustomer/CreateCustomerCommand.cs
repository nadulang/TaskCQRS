using System;
using MediatR;
using TaskCQRS.Domain.Entities;

namespace TaskCQRS.Application.UseCases.Customer.Command.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<CreateCustomerCommandDto>
    {
        public Customers Data { get; set; }
    }

    //public class CreateCustomerData
    //{
    //    public string full_name { get; set; }
    //    public string username { get; set; }
    //    public DateTime birthdate { get; set; }
    //    public string password { get; set; }
    //    public Gender gender { get; set; }
    //    public string email { get; set; }
    //    public string phone_number { get; set; }    
    //}
    //public enum Gender
    //{
    //    Male = 1,
    //    Female = 2
    //}
}

