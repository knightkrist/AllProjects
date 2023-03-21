using AllProjects.DataContext;
using AllProjects.Interface;
using AllProjects.Middleware;
using AllProjects.Models;
using AllProjects.Schema;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
string _policyName = "Policy";

builder.Services.AddCustomCorsMiddleware(_policyName);

builder.Services.AddSingleton<IHolidayRegistrationService, HolidayRegistrationService>();
builder.Services.AddSingleton<IGetStringsInterface, GetStringsInterface>();
builder.Services.AddSingleton<IDatabaseInterface, DatabaseInterface>();

builder.Services.AddDbContextFactory<SQLDataContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<SQLDataContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddGraphQLServer().AddQueryType<Query>().AddProjections().AddFiltering().AddSorting().AddHttpRequestInterceptor<CustomRequestInterceptor>();

builder.Services.AddRateLimiting(configuration);

var app = builder.Build();
app.UseCors(_policyName);
app.UseSecruityHeaders();
app.UseMiddleware<CustomIPRateLimitMiddleware>();
//app.MapGet("/", () => "Hello World!");

using (IServiceScope scope = app.Services.CreateScope())
{
    IDbContextFactory<SQLDataContext> contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SQLDataContext>>();

    using (SQLDataContext context = contextFactory.CreateDbContext())
    {
        context.Database.Migrate();
    }
}

app.MapProductEndpoints();

app.MapGraphQL("/data");


app.Run();

public sealed class CustomRequestInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(HttpContext context, IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder, CancellationToken cancellationToken)
    {
        if (context.Request.Headers.TryGetValue("Authorization", out var value))
        {
            requestBuilder.SetGlobalState("Authorization", (string)value);
        }
        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}