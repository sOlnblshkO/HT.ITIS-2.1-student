using System.Text;

namespace Hw8.MyMiddlewares;

public class MyExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            await context.Response.WriteAsync(message);
        }
    }
}