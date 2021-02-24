using PaymentProcessor.Data;
using PaymentProcessor.Domain.Models;
using PaymentProcessor.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace PaymentProcessor.Services
{
    public class PaymentStateService : Repository<PaymentState, long>, IPaymentStateService
    {
        private readonly PaymentProcessorContext _context;

        public PaymentStateService(PaymentProcessorContext context, ILogger<Repository<PaymentState, long>> logger)
            : base(context, logger)
        {
            _context = context;
        }
    }
}
