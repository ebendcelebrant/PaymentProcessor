using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PaymentProcessor.Domain.Responders;
using PaymentProcessor.Helpers;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentProcessor.Controllers
{
    public class BaseController : ControllerBase
    {
        [HttpOptions]
        public HttpResponseMessage Options()
        {
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            return response;
        }

        protected IActionResult BuildHttpResponse(Response response)
        {
            var status_code = response.HasErrors() ? 400 : 200;

            if (response.ResponseMessages.HasMessage<ExceptionWasThrown>())
            {
                status_code = 500;
            }

            return StatusCode(status_code, response);
        }

        protected IActionResult BuildHttpResponse(int statusCode)
        {
            return StatusCode(statusCode, new Response(Enumerable.Empty<IResponseMessage>()));
        }

        protected IActionResult BuildHttpResponse(int statusCode, Exception e)
        {
            return StatusCode(statusCode, new Response(new ExceptionWasThrown(e).ToEnumerable()));
        }
    }
}

