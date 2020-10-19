using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TheaterApplication.Utils.Exceptions;

namespace TheaterApplication.WebApi.ExceptionHandling
{
    public class HandlingExceptionMiddleware
    {
        private const int InternalServerErrorStatusCode = 500;

        private readonly RequestDelegate _next;

        public HandlingExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(HandledException ex)
            {
                context.Response.StatusCode = InternalServerErrorStatusCode;

                var body = JsonConvert.SerializeObject(ex.GetExceptionInfo());
                await context.Response.WriteAsync(body);
            }
        }
    }
}
