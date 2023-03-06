namespace AllProjects.Middleware
{
    internal static class CustomCorsMiddleware
    {
        internal static IServiceCollection AddCustomCorsMiddleware(this IServiceCollection services, string policyName)
        {
            services.AddCors(o => o.AddPolicy(policyName, builder =>
            {
                builder
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // Return the services
            return services;
        }
    }
}
