using System;
using System.Linq;
using System.Text;
using System.Net;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TaskCQRS.Infrastructure.Persistences;
using Hangfire;
using MimeKit;
using MailKit.Net.Smtp;

namespace TaskCQRS.Application.UseCases.Customer.Queries.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, GetCustomerDto>
    {
        private readonly EcommerceContext _context;

        public GetCustomerQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetCustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {

            BackgroundJob.Enqueue(() => Console.WriteLine("Someone's requesting and getting a data."));


            var result = await _context.CustomersData.FindAsync(request.id);
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
                    Text = @"You're requesting and getting a customer data."
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
                return new GetCustomerDto
                {
                    Success = true,
                    Message = "Customer succesfully retrieved",
                    Data = result
                };
            }

            


        }
    }
}
