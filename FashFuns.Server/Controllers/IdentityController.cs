using FashFuns.Common.WebApi;
using FashFuns.Domain.DataContracts.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using FashFuns.Common.Exceptions.UserExceptions;
using FashFuns.Common.ResponseBuilder.Contracts;
using FashFuns.Common.WebApi.RoutingConfiguration.Assets;
using FashFuns.Common.WebApi.RoutingConfiguration;
using FashFuns.Services.IdentityServices.Contracts;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace FashFuns.Server.Controllers
{
    [AssignControllerRoute(WebApiEnvironmnet.Current, WebApiVersion.ApiVersion1, ApplicationSegments.UserIdentity)]
    public class IdentityController : WebApiControllerBase
    {
        private readonly IUserIdentityService _userIdentityService;

        public IdentityController(IUserIdentityService userIdentityService,
             IResponseFactory responseFactory) : base(responseFactory)
        {
            _userIdentityService = userIdentityService;
        }

        [HttpPost]
        [AllowAnonymous]
        [AssignActionRoute(IdentitySegments.NEW_USER)]
        public async Task<IActionResult> NewUser([FromBody] AuthenticationDataContract authenticateDataContract)
        {
            try
            {
                if (authenticateDataContract == null) throw new ArgumentNullException("AuthenticationDataContract");

                UserAccount user = null; 
                    //await _userIdentityService.SignInAsync(authenticateDataContract);

                return Ok(SuccessResponseBody(user, "User logged in successfully"));
            }
            catch (InvalidSignInException exc)
            {
                return BadRequest(ErrorResponseBody(exc.GetUserMessageException, HttpStatusCode.BadRequest, exc.Body));
            }
            catch (Exception exc)
            {
                Log.Error(exc.Message);
                return BadRequest(ErrorResponseBody(exc.Message, HttpStatusCode.BadRequest));
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [AssignActionRoute(IdentitySegments.SIGN_IN)]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationDataContract authenticateDataContract)
        {
            try
            {
                if (authenticateDataContract == null) throw new ArgumentNullException("AuthenticationDataContract");

                UserAccount user = await _userIdentityService.SignInAsync(authenticateDataContract);

                return Ok(SuccessResponseBody(user, "User logged in successfully"));
            }
            catch (InvalidSignInException exc)
            {
                return BadRequest(ErrorResponseBody(exc.GetUserMessageException, HttpStatusCode.BadRequest, exc.Body));
            }
            catch (Exception exc)
            {
                Log.Error(exc.Message);
                return BadRequest(ErrorResponseBody(exc.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [AssignActionRoute(IdentitySegments.SIGN_UP)]
        public async Task<IActionResult> SignUp([FromBody] AuthorizationDataContract authorizationDataContract)
        {
            try
            {
                if (authorizationDataContract == null) throw new ArgumentNullException("AuthorizationDataContract");

                UserAccount user = await _userIdentityService.SignUpAsync(authorizationDataContract);

                return Ok(SuccessResponseBody(user, "User logged in successfully"));
            }
            catch (InvalidSignInException exc)
            {
                return BadRequest(ErrorResponseBody(exc.GetUserMessageException, HttpStatusCode.BadRequest, exc.Body));
            }
            catch (Exception exc)
            {
                Log.Error(exc.Message);
                return BadRequest(ErrorResponseBody(exc.Message, HttpStatusCode.BadRequest));
            }
        }
    }
}
