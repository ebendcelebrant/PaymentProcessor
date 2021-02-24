using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentProcessor.Domain.Services;

namespace PaymentProcessor.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPaymentService Payments { get; }
        IPaymentStateService PaymentStates { get; }

        int Complete();
    }
}
