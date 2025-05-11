using NLog;
using ProjectEmploee.Core.Entities;
using System.Diagnostics;

namespace ProjectEmploee.Api.Midllewars
{
    using Microsoft.AspNetCore.Http;

    using NLog;

    using System.Threading.Tasks;
    public class LogMidllewer

    {
        private readonly RequestDelegate _next;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public LogMidllewer(RequestDelegate next)

        {
            _next = next;

        }
        public async Task InvokeAsync(HttpContext context)

        {
            Logger.Info($"Incoming request: {context.Request.Method} {context.Request.Path}");

            await _next(context);
        }
    }

}
