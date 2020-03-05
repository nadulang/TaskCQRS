using System;
namespace TaskCQRS.Application.Models.Query
{
    public class BaseDto
    {
        public bool Success { set; get; }
        public string Message { set; get; }
    }
}
