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

namespace TaskCQRS.Application.UseCases.Customer.Queries.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, GetCustomersDto>
    {
        private readonly EcommerceContext _context;

        public GetCustomersQueryHandler(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<GetCustomersDto> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Someone's requesting and getting all data."));

            var data = await _context.CustomersData.ToListAsync();

            var result = data.Select(e => new Customers
            {
                id = e.id,
                full_name = e.full_name,
                username = e.username,
                birthdate = e.birthdate,
                password = e.password,
                gender = e.gender,
                email = e.email,
                phone_number = e.phone_number,
                created_at = e.created_at,
                updated_at = e.updated_at
                
            });

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Cantik-Cantik Ngiau", "cacangie45@gmail.com"));
            message.To.Add(new MailboxAddress("Cantik-Cantik Ngiau", "cacangie45@gmail.com"));
            message.Subject = "Requesting all data";

            message.Body = new TextPart("plain")
            {
                Text = @"You're requesting and getting all customers data."
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

            return new GetCustomersDto
            {
                Message = "Success retrieving data",
                Success = true,
                Data = result.ToList()
            };
        }
    }
}