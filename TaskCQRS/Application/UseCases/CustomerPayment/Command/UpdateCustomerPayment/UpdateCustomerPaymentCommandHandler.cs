using System;
using System.Threading;
using System.Threading.Tasks;
using TaskCQRS.Infrastructure.Persistences;
using TaskCQRS.Application.Models.Query;
using TaskCQRS.Domain.Entities;
using MediatR;

namespace TaskCQRS.Application.UseCases.CustomerPayment.Command.UpdateCustomerPayment
{
    public class UpdateCustomerPaymentCommandHandler : IRequestHandler<UpdateCustomerPaymentCommand, UpdateCustomerPaymentCommandDto>
    {
        private readonly EcommerceContext _context;

        public UpdateCustomerPaymentCommandHandler(EcommerceContext context)
        {
            _context = context;
        }
        public async Task<UpdateCustomerPaymentCommandDto> Handle(UpdateCustomerPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = _context.PaymentsData.Find(request.Data.id);

            if (payment == null)
            {
                return null;
            }

            else
            {

                payment.customer_id = request.Data.customer_id;
                payment.name_on_card = request.Data.name_on_card;
                payment.exp_month = request.Data.exp_month;
                payment.exp_year = request.Data.exp_year;
                payment.postal_code = request.Data.postal_code;
                payment.credit_card_number = request.Data.credit_card_number;
                var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
                payment.updated_at = (long)time;

                await _context.SaveChangesAsync(cancellationToken);

                return new UpdateCustomerPaymentCommandDto
                {
                    Success = true,
                    Message = "Customer successfully updated",
                };
            }
        }
    }
}
