using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TaskCQRS.Infrastructure.Persistences;
using Hangfire;
using System.Net;
using MimeKit;
using MailKit.Net.Smtp;
using System;

namespace TaskCQRS.Application.UseCases.Product.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductDto>
    {
        private readonly EcommerceContext _context;

        public GetProductQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Someone's requesting and getting a product data."));

            var result = await _context.ProductsData.FindAsync(request.Id);
            if (result == null)
            {
                return null;
            }
            else
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Cantik-Cantik Ngiau", "cacangie45@gmail.com"));
                message.To.Add(new MailboxAddress("Cantik-Cantik Ngiau", "cacangie45@gmail.com"));
                message.Subject = "Requesting a data";

                message.Body = new TextPart("plain")
                {
                    Text = @"You're requesting and getting a product data."
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
                return new GetProductDto
                {
                    Success = true,
                    Message = "Product succesfully retrieved",
                    Data = result
                };
            }
        }
    }
}
