using TaskCQRS.Domain.Entities;
using MediatR;

namespace TaskCQRS.Application.UseCases.Product.Command.UpdateProduct
{
    public class UpdateProductCommand : IRequest<UpdateProductCommandDto>
    {
        public Products Data { get; set; }
    }
}
