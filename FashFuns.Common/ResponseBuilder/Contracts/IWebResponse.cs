﻿using System.Net;

namespace FashFuns.Common.ResponseBuilder.Contracts
{
    public interface IWebResponse
    {
        object Body { get; set; }

        string Message { get; set; }

        HttpStatusCode StatusCode { get; set; }
    }
}
