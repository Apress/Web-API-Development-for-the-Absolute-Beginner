namespace ConferenceApi.Infrastructure.Middleware.Extensions
{
    public static class MiddlewareExtensions
    {

        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder application)
        {
            return application.UseMiddleware<SecurityHeadersMiddleware>();
        }
    }
}
