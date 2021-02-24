using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentProcessor.Domain.Models;
using PaymentProcessor.Domain.Interfaces;

namespace PaymentProcessor.Domain.Services
{
    public interface IPaymentStateService : IRepository<PaymentState, long>
    {
    }
}
