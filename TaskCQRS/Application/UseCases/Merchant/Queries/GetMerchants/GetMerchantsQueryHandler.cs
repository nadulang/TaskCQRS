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

namespace TaskCQRS.Application.UseCases.Merchant.Queries.GetMerchants
{
    public class GetMerchantsQueryHandler : IRequestHandler<GetMerchantsQuery, GetMerchantsDto>
    {
        private readonly EcommerceContext _context;

        public GetMerchantsQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetMerchantsDto> Handle(GetMerchantsQuery request, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Someone's requesting and getting all merchants data."));

            var data = await _context.MerchantsData.ToListAsync();

            var result = data.Select(e => new Merchants
            {
                id = e.id,
                name = e.name,
                image = e.image,
                address = e.address,
                rating = e.rating,
                created_at = e.created_at,
                updated_at = e.updated_at
            });

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Cantik-Cantik Ngiau", "cacangie45@gmail.com"));
            message.To.Add(new MailboxAddress("Cantik-Cantik Ngiau", "cacangie45@gmail.com"));
            message.Subject = "Requesting all data";

            message.Body = new TextPart("plain")
            {
                Text = @"You're requesting and getting all merchants data."
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

            return new GetMerchantsDto
            {
                Message = "Success retrieving data",
                Success = true,
                Data = result.ToList()
            };
        }
    }
}
