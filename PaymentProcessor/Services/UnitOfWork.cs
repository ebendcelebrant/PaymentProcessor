using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PaymentProcessor.Domain.Interfaces;
using PaymentProcessor.Data;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using PaymentProcessor.Domain.Services;

namespace PaymentProcessor.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentProcessorContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        private bool _disposed;

        public UnitOfWork(PaymentProcessorContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IPaymentService Payments { get; }
        public IPaymentStateService PaymentStates { get; }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (ValidationException validationError)
            {
                _logger.LogError($"Property: {validationError.Data} Error: {validationError.Message} Inner Exception: {validationError.InnerException}");

                Trace.TraceInformation($"Property: {validationError.Data} Error: {validationError.Message} Inner Exception: {validationError.InnerException}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            finally
            {
                Dispose();
            }
            return -99;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }


    }

}
