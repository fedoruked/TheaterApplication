using Microsoft.AspNetCore.Builder;

namespace TheaterApplication.WebApi.ExceptionHandling
{
    public static class ExceptionHandlingAppBuilderExtension
    {
        public static void UseExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<HandlingExceptionMiddleware>();
        }
    }
}
