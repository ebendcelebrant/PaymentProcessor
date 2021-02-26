using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentProcessor.Domain.Interfaces;
using PaymentProcessor.Domain.Models;

namespace PaymentProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        public IUnitOfWork _unitOfWork;
        public ILogger _logger;
        public PaymentController(IUnitOfWork unitOfWork, ILogger<PaymentController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [Route("ProcessPayment")]
        [HttpPost]
        public IActionResult ProcessPayment(Payment payment)
        {
            try
            {
                _logger.LogInformation($"API Request. Params: {JsonConvert.SerializeObject(payment)}");
                var response = _unitOfWork.Payments.ProcessPayment(payment);

                _logger.LogInformation($"API Response: {response}");

                return BuildHttpResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"API Exception: {ex.ToString()}");
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);

            }
        }
    }
}
