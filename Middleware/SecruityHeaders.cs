using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AllProjects.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SecruityHeaders
    {
        private readonly RequestDelegate _next;

        public SecruityHeaders(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            string origin = context.Request.Scheme + "://" + context.Request.Host.Value;
            context.Response.Headers.Add("Access-Control-Allow-Origin", origin);
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
            context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST");
            context.Response.Headers.Add("Cache-Control", "no-store");
            context.Response.Headers.Add("Pragma", "no-cache");
            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';img-src data: https:;object-src 'none'; script-src https://stackpath.bootstrapcdn.com/ 'self' 'unsafe-inline';style-src https://stackpath.bootstrapcdn.com/ 'self' 'unsafe-inline'; upgrade-insecure-requests;");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            context.Response.Headers.Add("Content-Type", "application/json");
            context.Response.Headers.Add("Strict-Transport-Security", "max-age=15638400; includeSubDomains;");
            context.Response.Headers.Add("Permissions-Policy", "fullscreen=(), geolocation=(self)");
            return _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SecruityHeadersExtensions
    {
        public static IApplicationBuilder UseSecruityHeaders(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecruityHeaders>();
        }
    }
}
