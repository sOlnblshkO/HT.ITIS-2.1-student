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

            context.Response.ContentType = "text/plain";
            context.Response.ContentLength = message.Length;
            await context.Response.WriteAsync(message);
        }
    }
}