using MediatR;
namespace TaskCQRS.Application.UseCases.Merchant.Queries.GetMerchant
{
    public class GetMerchantQuery : IRequest<GetMerchantDto>
    {
        public int id { get; set; }
    }
}
