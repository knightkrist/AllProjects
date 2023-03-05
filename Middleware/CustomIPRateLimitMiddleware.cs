using AllProjects.Helper;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;

namespace AllProjects.Middleware
{
    public class CustomIPRateLimitMiddleware : IpRateLimitMiddleware
    {
        public CustomIPRateLimitMiddleware(RequestDelegate next, IProcessingStrategy processingStrategy, IOptions<IpRateLimitOptions> options, IIpPolicyStore policyStore, IRateLimitConfiguration config, ILogger<IpRateLimitMiddleware> logger) : base(next, processingStrategy, options, policyStore, config, logger)
        {
        }

        public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
        {
            //  return base.ReturnQuotaExceededResponse(httpContext, rule, retryAfter);
            return ResourceHelper.ReturnQuotaExceededResponseText(httpContext, rule, retryAfter);
        }
    }

    // This is when we are using ClientRateLimitOptions on the RateLimitingMiddleware
    public class CustomClientRateLimitMiddleware : ClientRateLimitMiddleware
    {
        public CustomClientRateLimitMiddleware(RequestDelegate next, IProcessingStrategy processingStrategy, IOptions<ClientRateLimitOptions> options,
            IClientPolicyStore policyStore, IRateLimitConfiguration config, ILogger<ClientRateLimitMiddleware> logger) :
            base(next, processingStrategy, options, policyStore, config, logger)
        {
        }

        public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
        {
            return ResourceHelper.ReturnQuotaExceededResponseText(httpContext, rule, retryAfter);
        }

    }
}
