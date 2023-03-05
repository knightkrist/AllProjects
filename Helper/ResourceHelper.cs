using AllProjects.Models;
using AspNetCoreRateLimit;
using Newtonsoft.Json;
using System.Net;

namespace AllProjects.Helper
{
    public class ResourceHelper
    {
        public static Task ReturnQuotaExceededResponseText(HttpContext httpContext, RateLimitRule rule, string retryAfter)
        {
            string path = httpContext.Request.Path.Value;
            string str = "API calls quota exceeded! Maximum API limit is " + rule.Limit + " requests for every " + rule.Period;
            Status status = new Status() { status = false, statuscode = (int)HttpStatusCode.TooManyRequests, message = str };
            var result = JsonConvert.SerializeObject(status);
            httpContext.Response.Headers["Retry-After"] = retryAfter;
            httpContext.Response.StatusCode = 429;
            httpContext.Response.ContentType = "application/json";
            //WriteQuotaExceededResponseMetadata(path, retryAfter);
            return httpContext.Response.WriteAsync(result);
        }
    }
}
