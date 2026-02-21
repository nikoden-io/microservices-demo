var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddCarter(
    new DependencyContextAssemblyCatalog(assemblies: assembly)
);

builder.Services.AddMarten(opts => { opts.Connection(builder.Configuration.GetConnectionString("Database")!); })
    .UseLightweightSessions();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler();

app.Run();