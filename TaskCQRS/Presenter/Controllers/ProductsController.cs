using TaskCQRS.Application.UseCases.Product.Queries.GetProduct;
using TaskCQRS.Application.UseCases.Product.Queries.GetProducts;
using TaskCQRS.Application.UseCases.Product.Command.CreateProduct;
using TaskCQRS.Application.UseCases.Product.Command.DeleteProduct;
using TaskCQRS.Application.UseCases.Product.Command.UpdateProduct;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TaskCQRS.Domain.Entities;
using TaskCQRS.Infrastructure.Persistences;
using Microsoft.AspNetCore.Authorization;
using MediatR;

namespace TaskCQRS.Presenter.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/Products")]

    public class ProductsController : ControllerBase
    {
        private IMediator _mediatr;

        public ProductsController(IMediator mediatr)
        {
            _mediatr = mediatr;

        }

        [HttpGet]
        public async Task<ActionResult<GetProductsDto>> Get()
        {
            var result = new GetProductsQuery();
            return Ok(await _mediatr.Send(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> Get(int id)
        {
            
            var command = new GetProductQuery(id);
            var result = await _mediatr.Send(command);
            return result != null ? (ActionResult)Ok(new { Message = "success", data = result }) : NotFound(new { Message = "not found" });


        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommand data)
        {
            var result = await _mediatr.Send(data);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediatr.Send(command);

            return command != null ? (IActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "not found" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductCommand data)
        {
            try
            {
                data.Data.id = id;
                var result = await _mediatr.Send(data);
                return Ok(result);
            }

            catch (Exception)
            {
                return NotFound();
            }
        }

    }

       
}
