using AllProjects.DataContext;
using AllProjects.Models;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


    var connstring = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SQLDataContext>(o => o.UseSqlServer(connstring));

builder.Services.AddGraphQLServer().AddQueryType<Query>().AddProjections().AddFiltering().AddSorting().AddHttpRequestInterceptor<CustomRequestInterceptor>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

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