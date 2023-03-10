﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.Enums;

namespace For_A_Donation.Helpers.Attributes;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _roles;

    public AuthorizeAttribute(string[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (User)context.HttpContext.Items["User"];
        
        if (user == null)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }

        else
        {
            var role = user.Role.ToString();
            var res = _roles.FirstOrDefault(x => x == role);

            if (res == null)
            {
                context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}