using System;
namespace TaskCQRS.Domain.Entities
{
    public class Merchants
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string address { get; set; }
        public double rating { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
    }
}
