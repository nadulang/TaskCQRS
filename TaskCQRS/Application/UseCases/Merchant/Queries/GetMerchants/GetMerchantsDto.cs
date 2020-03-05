using TaskCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using TaskCQRS.Application.Models.Query;

namespace TaskCQRS.Application.UseCases.Merchant.Queries.GetMerchants
{
    public class GetMerchantsDto : BaseDto
    {
        public IList<Merchants> Data { get; set; }
    }
}
