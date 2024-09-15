using System.Diagnostics;

namespace MiddlewareSample
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation("Handling request: {0}", context.Request.Path);

            await _next(context);

            stopwatch.Stop();

            _logger.LogInformation("Finished handling request.");

            _logger.LogInformation($"Outgoing Response: {context.Response.StatusCode}. Request took: {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
