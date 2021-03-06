﻿using System.Net;
using FashFuns.Common.ResponseBuilder.Contracts;

namespace FashFuns.Common.ResponseBuilder
{
    public class ErrorResponse : IWebResponse
    {
        public object Body { get; set; }

        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
