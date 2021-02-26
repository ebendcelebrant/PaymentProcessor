using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentProcessor.Domain.Models;
using PaymentProcessor.Domain.Interfaces;
using PaymentProcessor.Domain.Responders;

namespace PaymentProcessor.Domain.Services
{
    public interface IPaymentService : IRepository<Payment, long>
    {
        Response ProcessPayment(Payment payment);
    }
}
