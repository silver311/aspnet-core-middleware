namespace MiddlewareSample
{
    public class AddHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public AddHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Append("X-My-Header", "MyCustomHeaderValue");
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
