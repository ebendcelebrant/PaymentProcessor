using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProcessor.Domain
{
    public class Enumerations
    {
        public enum PaymentStates : byte
        {
            Pending =1,
            Processed,
            Failed
        }
        public enum PaymentServices: byte
        {
            CheapPaymentService = 1,
            ExpensivePaymentService,
            PremiumPaymentService
        }
    }
}
