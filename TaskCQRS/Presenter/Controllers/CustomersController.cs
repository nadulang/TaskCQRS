using TaskCQRS.Application.UseCases.Customer.Queries.GetCustomer;
using TaskCQRS.Application.UseCases.Customer.Queries.GetCustomers;
using TaskCQRS.Application.UseCases.Customer.Command.CreateCustomer;
using TaskCQRS.Application.UseCases.Customer.Command.UpdateCustomer;
using TaskCQRS.Application.UseCases.Customer.Command.DeleteCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TaskCQRS.Domain.Entities;
using MediatR;
using System.Threading.Tasks;

namespace TaskCQRS.Presenter.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/Customer")]

    public class CustomersController : ControllerBase
    {
        private IMediator _mediatr;

        public CustomersController(IMediator mediatr)
        {
            _mediatr = mediatr;

        }

        [HttpGet]
        public async Task<ActionResult<GetCustomersDto>> Get()
        {
            var result = new GetCustomersQuery();
            return Ok(await _mediatr.Send(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerDto>> Get(int id)
        {
            var result = new GetCustomerQuery(id);
            return Ok(await _mediatr.Send(result));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerCommand data)
        {
            var result = await _mediatr.Send(data);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCustomerCommand(id);
            var result = await _mediatr.Send(command);

            return result != null ? (IActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "not found" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int ID, UpdateCustomerCommand data)
        {
            data.Data.id = ID;
            var result = await _mediatr.Send(data);
            return Ok(result);
        }



    }
}

    
