using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Infrastructure.Persistences;
using MediatR;
using System;

namespace TaskCQRS.Application.UseCases.CustomerPayment.Command.CreateCustomerPayment
{
    public class CreateCustomerPaymentCommandHandler : IRequestHandler<CreateCustomerPaymentCommand, CreateCustomerPaymentCommandDto>
    {
        private readonly EcommerceContext _context;

        public CreateCustomerPaymentCommandHandler(EcommerceContext context)
        {
            _context = context;
        }
        public async Task<CreateCustomerPaymentCommandDto> Handle(CreateCustomerPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Domain.Entities.CustomerPayments
            {
                customer_id = request.Data.customer_id,
                name_on_card = request.Data.name_on_card,
                exp_month = request.Data.exp_month,
                exp_year = request.Data.exp_year,
                postal_code = request.Data.postal_code,
                credit_card_number = request.Data.credit_card_number
            };

            _context.PaymentsData.Add(payment);
            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            payment.created_at = (long)time;
            payment.updated_at = (long)time;
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateCustomerPaymentCommandDto
            {
                Success = true,
                Message = "Your data succesfully created"
            };
        }
    }
}
