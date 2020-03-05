using MediatR;

namespace TaskCQRS.Application.UseCases.Merchant.Command.DeleteMerchant
{
    public class DeleteMerchantCommand : IRequest<DeleteMerchantCommandDto>
    {
        public int Id { get; set; }

        public DeleteMerchantCommand(int id)
        {
            Id = id;
        }
    }
}
