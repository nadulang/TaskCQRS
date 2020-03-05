using MediatR;
using TaskCQRS.Domain.Entities;

namespace TaskCQRS.Application.UseCases.Product.Command.CreateProduct
{
    public class CreateProductCommand : IRequest<CreateProductCommandDto>
    {
        public Products Data { get; set; }
    }
}
