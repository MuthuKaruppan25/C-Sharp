using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BookMicroservice.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LogMiddleware> _logger;

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> _logger)
        {
            this.next = next;
            this._logger = _logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            var request = context.Request;
            _logger.LogInformation("Handling request: {method} {url}", request.Method, request.Path);

            await next(context);

            stopwatch.Stop();
            _logger.LogInformation("Finished request in {duration} ms with status code {statusCode}",
                stopwatch.ElapsedMilliseconds,
                context.Response.StatusCode);
        }

    }
}