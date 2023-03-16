using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AnimalAPI.Helpers.Attributes;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RegistrationAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.Items["User"];

        if (user != null)
        {
            context.Result = new JsonResult(new { message = "User is authorized" }) { StatusCode = StatusCodes.Status403Forbidden };
        }
    }
}
