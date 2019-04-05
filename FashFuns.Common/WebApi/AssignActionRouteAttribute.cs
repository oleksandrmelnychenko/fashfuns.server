using Microsoft.AspNetCore.Mvc;
using System;

namespace FashFuns.Common.WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AssignActionRouteAttribute : RouteAttribute
    {

        public AssignActionRouteAttribute(string template) : base(template)
        {
        }
    }
}
