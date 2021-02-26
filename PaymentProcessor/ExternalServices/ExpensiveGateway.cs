using PaymentProcessor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProcessor.ExternalServices
{
    public class ExpensiveGateway : IExpensiveGateway
    {
        public bool DoPayment(Payment payment)
        {
            return true;
        }
    }
}
