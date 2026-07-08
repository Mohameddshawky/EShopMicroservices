using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplication()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();






app.Run();
