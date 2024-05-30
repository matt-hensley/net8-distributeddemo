using Grafana.OpenTelemetry;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry().UseGrafana();
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.UseGrafana();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(
    sp => ConnectionMultiplexer.Connect("redis:6379"));
builder.Services.AddScoped(
    sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapGet("/error", () =>
{
    throw new Exception("Always throws an error");
});
app.Run();
