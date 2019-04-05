using FashFuns.Common.ResponseBuilder.Contracts;

namespace FashFuns.Common.ResponseBuilder
{
    public class ResponseFactory : IResponseFactory
    {
        public IWebResponse GetSuccessReponse()
        {
            return new SuccessResponse();
        }

        public IWebResponse GetErrorResponse()
        {
            return new ErrorResponse();
        }
    }
}
