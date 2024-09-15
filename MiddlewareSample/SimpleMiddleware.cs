namespace MiddlewareSample
{
    public class SimpleMiddleware
    {
        private readonly RequestDelegate _next;

        public SimpleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("Hello from SimpleMiddleware\n");

            await _next(context);
        }
    }
}
