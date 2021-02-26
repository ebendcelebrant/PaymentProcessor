using PaymentProcessor.Domain.Responders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProcessor.Domain.Validations
{
    public class InvalidCreditCardNumber : AResponseErrorMessage
    {
        public override string Message => nameof(InvalidCreditCardNumber);
    }
    public class InvalidCreditCardInput : AResponseErrorMessage
    {
        public override string Message => nameof(InvalidCreditCardInput);
    }
    public class AmountIsNotPostive : AResponseErrorMessage
    {
        public override string Message => nameof(AmountIsNotPostive);
    }
    public class DateIsPast : AResponseErrorMessage
    {
        public override string Message => nameof(DateIsPast);
    }
    public class InvalidSecurityCode : AResponseErrorMessage
    {
        public override string Message => nameof(InvalidSecurityCode);
    }
}
