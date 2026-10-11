using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", limiterOptions =>
    {
        limiterOptions.PermitLimit = 5;
        limiterOptions.Window = TimeSpan.FromSeconds(10);
       
    });
});


var app = builder.Build();
app.UseRateLimiter();

app.MapReverseProxy();

app.MapReverseProxy();



app.MapGet("/", () => "Hello World!");

app.Run();
