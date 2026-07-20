using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplication()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();


var app = builder.Build();



app.UseApiService();
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
