using TaskCQRS.Domain.Entities;
using MediatR;

namespace TaskCQRS.Application.UseCases.Merchant.Command.UpdateMerchant
{
    public class UpdateMerchantCommand : IRequest<UpdateMerchantCommandDto>
    {
        public Merchants Data { get; set; }
    }
}
