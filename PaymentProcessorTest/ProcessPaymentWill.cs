using System;
using Xunit;
using PaymentProcessor.Services;
using PaymentProcessor.Data;
using PaymentProcessor.Domain.Models;
using PaymentProcessor.Domain.Responders;
using PaymentProcessor.ExternalServices;
using PaymentProcessorTest.Helpers;
using Microsoft.Extensions.Logging;
using Moq;

namespace PaymentProcessorTest
{
    public class ProcessPaymentWill
    {
        [Fact]
        public void NotAllowEmptyCardHolder()
        {
            var mockPaymentData = ProcessPaymentTest.CreatePayment(new Payment()
            {
                Amount = ProcessPaymentTest.Amount,
                CardHolder = "",
                CreditCardNumber = ProcessPaymentTest.CreditCardNumber,
                ExpirationDate = DateTime.Now.AddMonths(8),
                SecurityCode = "345"
            });

            var mockContext = new Mock<PaymentProcessorContext>();
            var mockLogger = new Mock<ILogger<Repository<Payment, long>>>();

            var sut = new PaymentService(mockContext.Object, mockLogger.Object);

            var response = sut.ProcessPayment(mockPaymentData);

            Assert.True(response.HasErrors());
        }

        [Fact]
        public void NotAllowPastExpirationDate()
        {
            var mockPaymentData = ProcessPaymentTest.CreatePayment(new Payment()
            {
                Amount = ProcessPaymentTest.Amount,
                CardHolder = ProcessPaymentTest.CardHolder,
                CreditCardNumber = ProcessPaymentTest.CreditCardNumber,
                ExpirationDate = DateTime.Now.AddMonths(-8),
                SecurityCode = "345"
            });

            var mockContext = new Mock<PaymentProcessorContext>();
            var mockLogger = new Mock<ILogger<Repository<Payment, long>>>();
            var mockCheapGateway = new Mock<ICheapGateway>();
            var mockExpensiveGateway = new Mock<IExpensiveGateway>();

            var sut = new PaymentService(mockContext.Object, mockLogger.Object);

            var response = sut.ProcessPayment(mockPaymentData);

            Assert.True(response.HasErrors());
        }
    }
}
