using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentProcessor.Domain.Interfaces;

namespace PaymentProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public IUnitOfWork _unitOfWork;
        public ILogger _logger;
        public PaymentController(IUnitOfWork unitOfWork, ILogger<PaymentController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [Route("FromJson/Display")]
        [HttpPost]
        public IActionResult DisplayFormattedOpeningHours(OpeningHours openingHours)
        {
            try
            {
                _logger.LogInformation($"API Request. Params: {JsonConvert.SerializeObject(openingHours)}");
                var response = _service.FormatOpeningHours(openingHours);


                _logger.LogInformation($"API Response: {response}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"API Exception: {ex.ToString()}");
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);

            }
        }
    }
}
