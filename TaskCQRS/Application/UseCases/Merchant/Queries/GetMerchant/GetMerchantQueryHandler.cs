using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TaskCQRS.Infrastructure.Persistences;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Hangfire;
using System;

namespace TaskCQRS.Application.UseCases.Merchant.Queries.GetMerchant
{
    public class GetMerchantQueryHandler : IRequestHandler<GetMerchantQuery, GetMerchantDto>
    {
        private readonly EcommerceContext _context;

        public GetMerchantQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetMerchantDto> Handle(GetMerchantQuery request, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Someone's requesting and getting a merchant data."));

            var result = await _context.MerchantsData.FindAsync(request.id);
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
                    Text = @"You're requesting and getting a merchant data."
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
        
                return new GetMerchantDto
                {
                    Success = true,
                    Message = "Merchant succesfully retrieved",
                    Data = result
                };
            }
        }
    }
}
