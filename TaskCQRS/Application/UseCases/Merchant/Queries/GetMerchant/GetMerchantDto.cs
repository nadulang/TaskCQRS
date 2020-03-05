using TaskCQRS.Domain.Entities;
using TaskCQRS.Application.Models.Query;

namespace TaskCQRS.Application.UseCases.Merchant.Queries.GetMerchant
{
      public class GetMerchantDto : BaseDto
        {
            public Merchants Data { get; set; }
        }
    
}
