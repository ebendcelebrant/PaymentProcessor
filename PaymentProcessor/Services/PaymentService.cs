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
    public class PaymentService : Repository<Payment, long>, IPaymentService
    {
        private readonly PaymentProcessorContext _context;

        public PaymentService(PaymentProcessorContext context, ILogger<Repository<Payment, long>> logger)
            : base(context, logger)
        {
            _context = context;
        }
    }
}
