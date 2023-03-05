using AspNetCoreRateLimit;

namespace AllProjects.Middleware
{
    internal static class RateLimitingMiddleware
    {
        internal static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            // Used to store rate limit counters and ip rules
            services.AddMemoryCache();

            // Load in general configuration from appsettings.json
            services.Configure<IpRateLimitOptions>(options => configuration.GetSection("IpRateLimitingSettings").Bind(options));

            // Inject Counter and Store Rules
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            services.AddInMemoryRateLimiting();

            //services.Configure<ClientRateLimitOptions>(options => configuration.GetSection("ClientRateLimitingSettings").Bind(options));

            // Inject Counter and Store Rules using Distributed Cache Store
            services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();

            services.AddDistributedRateLimiting();

            // Return the services
            return services;
        }

    }
}
