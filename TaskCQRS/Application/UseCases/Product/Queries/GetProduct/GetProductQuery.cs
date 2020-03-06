using MediatR;
namespace TaskCQRS.Application.UseCases.Product.Queries.GetProduct
{
    public class GetProductQuery : IRequest<GetProductDto>
    {
        public int Id { get; set; }

        public GetProductQuery(int id)
        {
            Id = id;
        }
    }
}
