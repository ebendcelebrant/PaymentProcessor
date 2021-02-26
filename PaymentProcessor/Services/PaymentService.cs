using PaymentProcessor.Data;
using PaymentProcessor.Domain.Models;
using PaymentProcessor.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaymentProcessor.Domain.Responders;
using PaymentProcessor.Domain.Validations;
using static PaymentProcessor.Domain.Enumerations;
using PaymentProcessor.ExternalServices;
using static PaymentProcessor.Helpers.ConstantWebAPI;
using PaymentProcessor.Helpers;

namespace PaymentProcessor.Services
{
    public class PaymentService : Repository<Payment, long>, IPaymentService
    {
        private readonly PaymentProcessorContext _context;
        private readonly ILogger _logger;

        public PaymentService(PaymentProcessorContext context, ILogger<Repository<Payment, long>> logger)
            : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public Response ProcessPayment(Payment payment)
        {
            try
            {
                var messages = Validate(payment);

                if (messages.HasErrors())
                {
                    return new Response(messages);
                }

                AddPayment(payment);

                bool serviceResponse = MakePayment(payment, false);

                UpdatePayment(payment, serviceResponse);

                return Response.Success();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Service Exception: {ex.ToString()}");
                return new Response(new ExceptionWasThrown(ex).ToEnumerable());
            }

        }

        private bool AddPayment(Payment payment)
        {
            payment.PaymentState.State = PaymentStates.Pending.ToString();

            Add(payment);
            Complete();

            return true;
        }

        private bool UpdatePayment(Payment payment, bool paymentStatus)
        {
            payment.PaymentState.State = paymentStatus ?
                PaymentStates.Processed.ToString() : PaymentStates.Pending.ToString();

            Update(payment);
            Complete();

            return true;
        }

        private bool MakePayment(Payment payment, bool serviceResponse)
        {
            //Consumption of Gateways will be simulated here rather than injected as they are 
            //expected to be external

            ICheapGateway cheapGateway = new CheapGateway();
            IExpensiveGateway expensiveGateway = new ExpensiveGateway();

            switch (SelectPaymentService(payment.Amount))
            {
                case PaymentServices.PremiumPaymentService:
                    serviceResponse = PremiumPaymentService(payment);
                    if (serviceResponse)
                        payment.PaymentState.State = PaymentStates.Processed.ToString();
                    else
                        for (int i = 0; i < PremiumPaymentRetries; i++)
                        {
                            if (!serviceResponse)
                                serviceResponse = PremiumPaymentService(payment);
                            else
                                break;
                        }
                    break;
                case PaymentServices.ExpensivePaymentService:
                    serviceResponse = expensiveGateway.DoPayment(payment);
                    if (serviceResponse)
                        payment.PaymentState.State = PaymentStates.Processed.ToString();
                    else
                        for (int i = 0; i < CheapGatewayRetries; i++)
                        {
                            if (!serviceResponse)
                                serviceResponse = cheapGateway.DoPayment(payment);
                            else
                                break;
                        }
                    break;
                case PaymentServices.CheapPaymentService:
                    serviceResponse = cheapGateway.DoPayment(payment);
                    break;

            }
            return serviceResponse;
        }

        private IEnumerable<IResponseMessage> Validate(Payment payment)
        {
            Guard.IsNotNull(payment.CreditCardNumber, nameof(payment.CreditCardNumber));
            Guard.IsNotNull(payment.CardHolder, nameof(payment.CardHolder));
            Guard.IsNotNull(payment.ExpirationDate, nameof(payment.ExpirationDate));
            Guard.IsNotNull(payment.Amount, nameof(payment.Amount));

            return new PremiseValidator()
                .For(payment.CreditCardNumber)
                    .TextIsNumeric<InvalidCreditCardInput>()
                    .TextIsCreditCardNumber<InvalidCreditCardNumber>()
                .For(payment.Amount)
                    .Must<AmountIsNotPostive>(_ => payment.Amount > 0)
                .For(payment.ExpirationDate)
                    .DateIsInFuture<DateIsPast>()
                .For(payment.SecurityCode)
                    .Must<InvalidSecurityCode>(_ => 
                        (!String.IsNullOrEmpty(payment.SecurityCode)) ?
                            payment.SecurityCode.All(char.IsNumber) : true)
                .GetFailures();
            
        }
        private bool PremiumPaymentService(Payment payment)
        {
            return true;
        }

        private PaymentServices SelectPaymentService(decimal amount)
        {
            if (amount < 20)
                return PaymentServices.CheapPaymentService;
            else if (amount > 21 && amount <= 500)
                return PaymentServices.ExpensivePaymentService;
            else
                return PaymentServices.PremiumPaymentService;
        }
    }
}
