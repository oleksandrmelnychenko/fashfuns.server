using FashFuns.Common.ResponseBuilder.Contracts;
using FashFuns.Common.WebApi;
using FashFuns.Common.WebApi.RoutingConfiguration;
using FashFuns.Common.WebApi.RoutingConfiguration.Assets;
using FashFuns.Domain.DataContracts.Products;
using FashFuns.Domain.Entities.Products;
using FashFuns.Services.IdentityServices.Contracts;
using FashFuns.Services.ProductServices.Contracts;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FashFuns.Server.Controllers
{
    [AssignControllerRoute(WebApiEnvironmnet.Current, WebApiVersion.ApiVersion1, ApplicationSegments.Products)]
    public class ProductController : WebApiControllerBase
    {
        private IShoppingCartService _shoppingCartService;

        public ProductController(
            IShoppingCartService shoppingCartService,
            IResponseFactory responseFactory) : base(responseFactory)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost]
        [AssignActionRoute(ProductSegments.CHANGE_SHOPPING_CART)]
        public IActionResult ChangeShoppingCart(long? shoppingCartId, List<OrderItem> orderItems)
        {

            return Ok();
        }

        [HttpGet]
        [AssignActionRoute(ProductSegments.GET_SHOPPING_CART)]
        public async Task<IActionResult> GetShoppingCart(long userId)
        {
            try
            {
                //TODO: if userId isn't existing get NotFound

                ShoppingCartInfo cart = await _shoppingCartService.GetCart(userId);

                return Ok(cart);
            }
            catch (Exception exc)
            {
                Log.Error(exc.Message);
                return BadRequest(ErrorResponseBody(exc.Message, HttpStatusCode.BadRequest));
            }

        }
    }
}
