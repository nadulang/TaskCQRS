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

namespace TaskCQRS.Presenter.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/Products")]

    public class ProductsController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public ProductsController(EcommerceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var pro = _context.ProductsData;
            return Ok(new { message = "success retrieve data", status = true, data = pro });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var getit = _context.ProductsData.First(e => e.id == id);
                _context.SaveChanges();
                return Ok(new { message = "success retrieve data", status = true, data = getit });
            }

            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public IActionResult Post(Products1 p)
        {
            var pro = p.data.attributes;
            _context.ProductsData.Add(pro);

            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            pro.created_at = (long)time;
            pro.updated_at = (long)time;
            _context.SaveChanges();

            return Ok(new { message = "success retrieve data", status = true, data = pro });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var del = _context.ProductsData.First(e => e.id == id);

                _context.ProductsData.Remove(del);
                _context.SaveChanges();

                return Ok("Your data has been deleted.");
            }

            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Products1 p)
        {
            var pro = p.data.attributes;

            try
            {
                var g = _context.ProductsData.Find(id);

                g.merchant_id = pro.merchant_id;
                g.name = pro.name;
                g.price = pro.price;
                var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
                g.updated_at = (long)time;

                _context.SaveChanges();

                var o = new Products
                {
                    id = g.id,
                    merchant_id = pro.merchant_id,
                    name = pro.name,
                    price = pro.price,
                    created_at = g.created_at,
                    updated_at = g.updated_at
                };
                return Ok(new { message = "success retrieve data", status = true, data = o });
            }

            catch (Exception)
            {
                return NotFound();
            }

        }

        public class Products1
        {
            public Attributes4 data { get; set; }
        }

        public class Attributes4
        {
            public Products attributes { get; set; }
        }
    }
}
