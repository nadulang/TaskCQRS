using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TaskCQRS.Infrastructure.Persistences;
using System;
using Hangfire;
using System.Net;
using MimeKit;
using MailKit.Net.Smtp;

namespace TaskCQRS.Application.UseCases.CustomerPayment.Queries.GetCustomerPayment
{
    public class GetCustomerPaymentQueryHandler : IRequestHandler<GetCustomerPaymentQuery, GetCustomerPaymentDto>
    {
        private readonly EcommerceContext _context;

        public GetCustomerPaymentQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetCustomerPaymentDto> Handle(GetCustomerPaymentQuery request, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Someone's requesting and getting a payment data."));

            var result = await _context.PaymentsData.FindAsync(request.id);
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
                    Text = @"You're requesting and getting a payment data."
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

                return new GetCustomerPaymentDto
                {
                    Success = true,
                    Message = "Payment succesfully retrieved",
                    Data = result
                };
            }

        }
    }
    
        
    
}
