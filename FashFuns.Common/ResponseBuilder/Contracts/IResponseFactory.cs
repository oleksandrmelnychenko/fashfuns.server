﻿

namespace FashFuns.Common.ResponseBuilder.Contracts
{
    public interface IResponseFactory
    {
        IWebResponse GetSuccessReponse();

        IWebResponse GetErrorResponse();
    }
}
