using MediatR;
namespace TaskCQRS.Application.UseCases.Product.Queries.GetProduct
{
    public class GetProductQuery : IRequest<GetProductDto>
    {
        public int id { get; set; }
    }
}
