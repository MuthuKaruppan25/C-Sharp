using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BookMicroservice.Middleware
{
    public class MyCustomFilter : IAsyncActionFilter
    {
        private readonly ILogger<MyCustomFilter> _logger;

        public MyCustomFilter(ILogger<MyCustomFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("➡️ MyCustomFilter executing before action: {Path}", context.HttpContext.Request.Path);
            var resultContext = await next();
            _logger.LogInformation("⬅️ MyCustomFilter executed after action");
        }
    }
}