using TaskCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using TaskCQRS.Application.Models.Query;

namespace TaskCQRS.Application.UseCases.Product.Queries.GetProducts
{
    public class GetProductsDto : BaseDto
    {
        public IList<Products> Data { get; set; }
    }
}
