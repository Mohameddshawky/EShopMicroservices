using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using CatalogApi.Data;
using FluentValidation;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));

    });

builder.Services.AddValidatorsFromAssembly(assembly);       
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("MartenDb")!);

    
}).UseLightweightSessions();
if(builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogIntialData>();
}
builder.Services.AddExceptionHandler<CustomExceptionHandler>(); 
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(op => { });

app.Run();
