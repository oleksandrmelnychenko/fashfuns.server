using System.Net;
using FashFuns.Common.ResponseBuilder.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FashFuns.Common.WebApi
{
    public abstract class WebApiControllerBase : Controller
    {
        private readonly IResponseFactory _responseFactory;

        /// <summary>
        ///     ctor().
        /// </summary>
        protected WebApiControllerBase(IResponseFactory responseFactory)
        {
            _responseFactory = responseFactory;
        }

        protected IWebResponse SuccessResponseBody(object body, string message = "")
        {
            IWebResponse response = _responseFactory.GetSuccessReponse();

            response.Body = body;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = message;

            return response;
        }

        protected IWebResponse ErrorResponseBody(string message, HttpStatusCode statusCode,object body = null)
        {
            IWebResponse response = _responseFactory.GetErrorResponse();

            response.Message = message;
            response.StatusCode = statusCode;
            response.Body = body;

            return response;
        }
    }
}
