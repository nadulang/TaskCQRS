using TaskCQRS.Application.UseCases.Merchant.Queries.GetMerchant;
using TaskCQRS.Application.UseCases.Merchant.Queries.GetMerchants;
using TaskCQRS.Application.UseCases.Merchant.Command.CreateMerchant;
using TaskCQRS.Application.UseCases.Merchant.Command.DeleteMerchant;
using TaskCQRS.Application.UseCases.Merchant.Command.UpdateMerchant;
using System.Net.NetworkInformation;
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
    [Authorize]
    [Route("api/Merchants")]

    public class MerchantsController : ControllerBase
    {
        private IMediator _mediatr;

        public MerchantsController(IMediator mediatr)
        {
            _mediatr = mediatr;

        }


        [HttpGet]
        public async Task<ActionResult<GetMerchantsDto>> Get()
        {
            var result = new GetMerchantsQuery();
            return Ok(await _mediatr.Send(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetMerchantDto>> Get(int id)
        {
  
            var command = new GetMerchantQuery(id);
            var result = await _mediatr.Send(command);
            return command != null ? (ActionResult)Ok(new { Message = "success", data = result }) : NotFound(new { Message = "not found" });

        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateMerchantCommand data)
        {
            var result = await _mediatr.Send(data);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteMerchantCommand(id);
            var result = await _mediatr.Send(command);

            return command != null ? (IActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "not found" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int ID, UpdateMerchantCommand data)
        {
            try
            {
                data.Data.id = ID;
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
