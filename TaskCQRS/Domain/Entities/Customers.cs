using System;
namespace TaskCQRS.Domain.Entities
{
    public class Customers
    {
        public int id { get; set; }
        public string full_name { get; set; }
        public string username { get; set; }
        public DateTime birthdate { get; set; }
        public string password { get; set; }
        public Gender gender { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
    }
    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    //public class CustomerResponse
    //{
    //    public int id { get; set; }
    //    public string full_name { get; set; }
    //    public string username { get; set; }
    //    public DateTime birthdate { get; set; }
    //    public string password { get; set; }
    //    public string gender { get; set; }
    //    public string email { get; set; }
    //    public string phone_number { get; set; }
    //    public long created_at { get; set; }
    //    public long updated_at { get; set; }
    //}

}

