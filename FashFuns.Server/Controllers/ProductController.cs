using System.Collections.Generic;
using FashFuns.Common.ResponseBuilder.Contracts;
using FashFuns.Common.WebApi;
using FashFuns.Common.WebApi.RoutingConfiguration;
using FashFuns.Common.WebApi.RoutingConfiguration.Assets;
using FashFuns.Domain.Entities.Products;
using Microsoft.AspNetCore.Mvc;

namespace FashFuns.Server.Controllers
{
    [AssignControllerRoute(WebApiEnvironmnet.Current, WebApiVersion.ApiVersion1, ApplicationSegments.Products)]
    public class ProductController : WebApiControllerBase
    {
        public ProductController(IResponseFactory responseFactory) : base(responseFactory)
        {

        }

        [HttpPost]
        [AssignActionRoute(ProductSegments.CHANGE_SHOPPING_CART)]
        public IActionResult ChangeShoppingCart(long? shoppingCartId, List<OrderItem> orderItems)
        {

            return Ok();
        }

        [HttpGet]
        [AssignActionRoute(ProductSegments.GET_SHOPPING_CART)]
        public IActionResult GetShoppingCart(long? userId)
        {


            return Ok();
        }
    }
}
