using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProcessor.Domain.Models
{
    public class PaymentState
    {
        public long Id { get; set; }
        public string State { get; set; }
        public virtual Payment Payment {get;set;}
    }
}
