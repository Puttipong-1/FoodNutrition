using FoodNutrition.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Helper
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Admin admin = (Admin)context.HttpContext.Items["Admin"];
            if(admin is null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) {StatusCode=StatusCodes.Status401Unauthorized};
            }
        }

    }
}
