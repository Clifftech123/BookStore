namespace BookStore.Presentation.middleware;


public class CustomUnauthorizedResponseMiddleware
{
    private readonly RequestDelegate _next;

    public CustomUnauthorizedResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == 401) // Unauthorized
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 401; 
            var response = new { message = "You are not authorized." };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}