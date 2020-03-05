using MediatR;
using TaskCQRS.Domain.Entities;

namespace TaskCQRS.Application.UseCases.Merchant.Command.CreateMerchant
{
    public class CreateMerchantCommand : IRequest<CreateMerchantCommandDto>
    {
        public Merchants Data { get; set; }
    }
}
