

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCarter();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));

});

var app = builder.Build();

app.MapCarter();

app.Run();
