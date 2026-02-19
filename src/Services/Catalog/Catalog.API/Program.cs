var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter(
    new DependencyContextAssemblyCatalog(assemblies: typeof(Program).Assembly)
);

builder.Services.AddMarten(opts => { opts.Connection(builder.Configuration.GetConnectionString("Database")!); })
    .UseLightweightSessions();

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });

var app = builder.Build();

app.MapCarter();

app.Run();