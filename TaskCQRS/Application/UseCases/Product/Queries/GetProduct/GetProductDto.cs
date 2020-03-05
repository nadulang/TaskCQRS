using TaskCQRS.Domain.Entities;
using TaskCQRS.Application.Models.Query;

namespace TaskCQRS.Application.UseCases.Product.Queries.GetProduct
{
    public class GetProductDto : BaseDto
    {
        public Products Data { get; set; }
    }
}
