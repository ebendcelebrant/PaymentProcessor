using System;
using System.Collections.Generic;
using System.Text;
using PaymentProcessor.Domain.Models;

namespace PaymentProcessorTest.Helpers
{
    public class ProcessPaymentTest
    {
        public const string CreditCardNumber = "4111111111111111";

        public const string CardHolder = "Aruorihwo Asagbra";
        public const decimal Amount = 150;

        public static Payment CreatePayment(Payment payment)
        {
            if (payment == null)
            {
                payment = new Payment()
                {
                    Amount = Amount,
                    CardHolder = CardHolder,
                    CreditCardNumber = CreditCardNumber,
                    ExpirationDate = DateTime.Now.AddMonths(8)
            };
            }
            return payment;
        }
    }
}
