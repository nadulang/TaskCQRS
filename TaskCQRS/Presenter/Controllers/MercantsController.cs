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

namespace TaskCQRS.Presenter.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Merchants")]

    public class MercantsController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public MercantsController(EcommerceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var merch = _context.MerchantsData;
            return Ok(new { message = "success retrieve data", status = true, data = merch });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var getit = _context.MerchantsData.First(e => e.id == id);
                _context.SaveChanges();
                return Ok(new { message = "success retrieve data", status = true, data = getit });
            }

            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public IActionResult Post(Merch m)
        {
            var merchs = m.data.attributes;

            _context.MerchantsData.Add(merchs);
            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            merchs.created_at = (long)time;
            merchs.updated_at = (long)time;
            _context.SaveChanges();

            return Ok(new { message = "success retrieve data", status = true, data = merchs });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var del = _context.MerchantsData.First(e => e.id == id);

                _context.MerchantsData.Remove(del);
                _context.SaveChanges();

                return Ok("Your data has been deleted.");
            }

            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Merch m)
        {
            var merchs = m.data.attributes;

            try
            {
                var g = _context.MerchantsData.Find(id);

                g.name = merchs.name;
                g.image = merchs.image;
                g.address = merchs.address;
                g.rating = merchs.rating;
                var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
                g.updated_at = (long)time;

                _context.SaveChanges();

                var y = new Merchants
                {
                    id = g.id,
                    name = merchs.name,
                    image = merchs.image,
                    address = merchs.address,
                    rating = merchs.rating,
                    created_at = g.created_at,
                    updated_at = g.updated_at
                };
                return Ok(new { message = "success retrieve data", status = true, data = y });
            }

            catch (Exception)
            {
                return NotFound();
            }
        }
    }

    public class Merch
    {
        public Attributes3 data { get; set; }
    }

    public class Attributes3
    {
        public Merchants attributes { get; set; }
    }
}
