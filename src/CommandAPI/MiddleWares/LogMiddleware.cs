using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommandDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CommandAPI.MiddleWares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public LogMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LogMiddleware>();
        }
        public async Task InvokeAsync(HttpContext context, ILogStorage logStorage)
        {
            var log = new LogModel
            {
                Path = context.Request.Path,
                Method = context.Request.Method,
                QueryString = context.Request.QueryString.ToString()
            };
            if (context.Request.Method == "POST")
            {
                context.Request.EnableBuffering();
                var body = await new StreamReader(context.Request.Body)
                    .ReadToEndAsync();
                context.Request.Body.Position = 0;
                log.RequestBody = body;
            }
            log.RequestedOn = DateTime.Now;

            var originalBodyStream = context.Response.Body;

            using (var responseBodyStream = new MemoryStream())
            {
                context.Response.Body = responseBodyStream;

                await _next.Invoke(context);

                responseBodyStream.Position = 0;
                var response = await new StreamReader(responseBodyStream)
                    .ReadToEndAsync();
                responseBodyStream.Position = 0;

                log.Response = response;
                log.ResponseCode = context.Response.StatusCode.ToString();
                log.RespondedOn = DateTime.Now;

                logStorage.Store(log);

                await responseBodyStream.CopyToAsync(originalBodyStream);
            }
            // try
            // {
            //     await _next(context);
            // }
            // finally
            // {
            //     _logger.LogInformation(
            //         "Request {header.Value} {method} {url} => {statusCode}",
            //         context.Request?.Headers,
            //         context.Request?.Method,
            //         context.Request?.Path.Value,
            //         context.Response?.StatusCode);
            // }
        }
    }
}