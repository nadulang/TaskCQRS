using System;
namespace TaskCQRS.Application.UseCases.Customer.Models
{
    public class CustomerData1
    {
        public string Full_name { get; set; }
        public string Username { get; set; }
        public DateTime Birthdate { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Phone_number { get; set; }
        public long Created_at { get; set; }
        public long Updated_at { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}

