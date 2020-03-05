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
using Microsoft.AspNetCore.Authorization;
using TaskCQRS.Infrastructure.Persistences;

namespace TaskCQRS.Presenter.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Payment")]

    public class CustomersPaymentController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public CustomersPaymentController(EcommerceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var cust = _context.PaymentsData;
            return Ok(new { message = "success retrieve data", status = true, data = cust });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var getit = _context.PaymentsData.First(e => e.id == id);
                _context.SaveChanges();
                return Ok(new { message = "success retrieve data", status = true, data = getit });
            }

            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public IActionResult Post(Payments custs)
        {
            var customer = custs.data.attributes;
            _context.PaymentsData.Add(customer);
            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            customer.created_at = (long)time;
            customer.updated_at = (long)time;
            _context.SaveChanges();
            return Ok(new { message = "success retrieve data", status = true, data = customer });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var del = _context.PaymentsData.First(e => e.id == id);
                _context.PaymentsData.Remove(del);
                _context.SaveChanges();
                return Ok("Your data has been deleted.");
            }

            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Payments cust)
        {
            var customer = cust.data.attributes;

            try
            {
                var g = _context.PaymentsData.Find(id);
                g.customer_id = customer.customer_id;
                g.name_on_card = customer.name_on_card;
                g.exp_month = customer.exp_month;
                g.exp_year = customer.exp_year;
                g.postal_code = customer.postal_code;
                g.credit_card_number = customer.credit_card_number;

                var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
                g.updated_at = (long)time;

                _context.SaveChanges();

                var w = new CustomerPayments
                {
                    id = g.id,
                    customer_id = customer.customer_id,
                    name_on_card = customer.name_on_card,
                    exp_month = customer.exp_month,
                    exp_year = customer.exp_year,
                    postal_code = customer.postal_code,
                    credit_card_number = customer.credit_card_number,
                    created_at = g.created_at,
                    updated_at = g.updated_at
                };

                return Ok(new { message = "success retrieve data", status = true, data = w });
            }

            catch (Exception)
            {
                return NotFound();
            }
        }
    }

    public class Payments
    {
        public Attributes2 data { get; set; }
    }

    public class Attributes2
    {
        public CustomerPayments attributes { get; set; }
    }

}
