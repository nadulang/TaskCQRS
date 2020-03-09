using TaskCQRS.Application.UseCases.CustomerPayment.Queries.GetCustomerPayment;
using TaskCQRS.Application.UseCases.CustomerPayment.Queries.GetCustomerPayments;
using TaskCQRS.Application.UseCases.CustomerPayment.Command.CreateCustomerPayment;
using TaskCQRS.Application.UseCases.CustomerPayment.Command.DeleteCustomerPayment;
using TaskCQRS.Application.UseCases.CustomerPayment.Command.UpdateCustomerPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TaskCQRS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using MediatR;

namespace TaskCQRS.Presenter.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Payment")]

    public class CustomersPaymentController : ControllerBase
    {
        private IMediator _mediatr;

        public CustomersPaymentController(IMediator mediatr)
        {
            _mediatr = mediatr;

        }


        [HttpGet]
        public async Task<ActionResult<GetCustomerPaymentsDto>> Get()
        {
            var result = new GetCustomerPaymentsQuery();
            return Ok(await _mediatr.Send(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerPaymentDto>> Get(int id)
        {
            var command = new GetCustomerPaymentQuery(id);
            var result = await _mediatr.Send(command);

            return result != null ? (ActionResult)Ok(new { Message = "success", data = result }) : NotFound(new { Message = "not found" });
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerPaymentCommand data)
        {
            var result = await _mediatr.Send(data);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCustomerPaymentCommand(id);
            var result = await _mediatr.Send(command);

            return command != null ? (IActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "not found" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCustomerPaymentCommand data)
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
