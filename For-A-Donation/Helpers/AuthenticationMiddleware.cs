using For_A_Donation.Services.Interfaces;
using System.Text;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Helpers;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly ILogger _logger;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserService userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            context.Items["User"] = GetUser(token, userService);
        }

        await _next(context);
    }

    public User? GetUser(string token, IUserService userService)
    {
        try
        {
            string[] tokenDecode = TokenDecode(token);
            var user = userService.Get(tokenDecode[0], tokenDecode[1]);

            return user;
        }
        catch (NotFoundException)
        {
            // log
            return null;
        }
    }

    public string[] TokenDecode(string token)
    {
        var base64EncodedBytes = Convert.FromBase64String(token);
        string[] res = Encoding.UTF8.GetString(base64EncodedBytes).Split(':');
        return res;
    }
}
