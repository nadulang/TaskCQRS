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

namespace TaskCQRS.Application.UseCases.CustomerPayment.Queries.GetCustomerPayments
{
    public class GetCustomerPaymentsQueryHandler : IRequestHandler<GetCustomerPaymentsQuery, GetCustomerPaymentsDto>
    {
        private readonly EcommerceContext _context;

        public GetCustomerPaymentsQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetCustomerPaymentsDto> Handle(GetCustomerPaymentsQuery request, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Someone's requesting and getting all payments data."));

            var data = await _context.PaymentsData.ToListAsync();

            var result = data.Select(e => new CustomerPayments
            {
                id = e.id,
                customer_id = e.customer_id,
                name_on_card = e.name_on_card,
                exp_month = e.exp_month,
                exp_year = e.exp_year,
                postal_code = e.postal_code,
                credit_card_number = e.credit_card_number,
                created_at = e.created_at,
                updated_at = e.updated_at
            });

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Cantik-Cantik Ngiau", "cacangie45@gmail.com"));
            message.To.Add(new MailboxAddress("Cantik-Cantik Ngiau", "cacangie45@gmail.com"));
            message.Subject = "Requesting all data";

            message.Body = new TextPart("plain")
            {
                Text = @"You're requesting and getting all payments data."
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

            return new GetCustomerPaymentsDto
            {
                Message = "Success retrieving data",
                Success = true,
                Data = result.ToList()
            };
        }
    }
}
