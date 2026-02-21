var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services
    .AddPresentationServices(assembly)
    .AddApplicationServices(assembly)
    .AddInfrastructureServices(builder.Configuration)
    .AddCrossCuttingServices(builder.Configuration);

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler();
app.Run();