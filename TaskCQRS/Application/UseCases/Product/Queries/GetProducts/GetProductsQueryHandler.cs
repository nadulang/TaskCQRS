using System;
using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Application.Models.Query;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskCQRS.Domain.Entities;
using TaskCQRS.Infrastructure.Persistences;
using Hangfire;
using MimeKit;
using MailKit.Net.Smtp;

namespace TaskCQRS.Application.UseCases.Product.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsDto>
    {
        private readonly EcommerceContext _context;

        public GetProductsQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetProductsDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Someone's requesting and getting all products data."));

            var data = await _context.ProductsData.ToListAsync();

            var result = data.Select(e => new Products
            {
                id = e.id,
                merchant_id = e.merchant_id,
                name = e.name,
                price = e.price,
                created_at = e.created_at,
                updated_at = e.updated_at
            });

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Cantik-Cantik Ngiau", "cacangie45@gmail.com"));
            message.To.Add(new MailboxAddress("Cantik-Cantik Ngiau", "cacangie45@gmail.com"));
            message.Subject = "Requesting all data";

            message.Body = new TextPart("plain")
            {
                Text = @"You're requesting and getting all products data."
            };

            using (var client = new SmtpClient())
            {

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.mailtrap.io", 2525, false);


                client.Authenticate("84b015139889ab", "a7eda17f7b7703");

                client.Send(message);
                client.Disconnect(true);
                Console.WriteLine("E-mail Sent");
            }

            return new GetProductsDto
            {
                Message = "Success retrieving data",
                Success = true,
                Data = result.ToList()
            };
        }
    }
}
