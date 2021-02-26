using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentProcessor.Domain.Models;

namespace PaymentProcessor.ExternalServices
{
    public interface ICheapGateway
    {
        public bool DoPayment(Payment payment);
    }
}
