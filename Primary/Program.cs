using Grafana.OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

/* Using upstream OpenTelemetry package
builder.Services.AddOpenTelemetry()
    .WithTracing(configure =>
    {
        configure.AddAspNetCoreInstrumentation();
        configure.AddHttpClientInstrumentation();
        configure.AddOtlpExporter();
    })
    .WithMetrics(configure =>
    {
        configure.AddAspNetCoreInstrumentation();
        configure.AddHttpClientInstrumentation();
        configure.AddProcessInstrumentation();
        configure.AddRuntimeInstrumentation();
        configure.AddOtlpExporter();
    });
*/

builder.Services.AddOpenTelemetry().UseGrafana();
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.UseGrafana();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.Run();
