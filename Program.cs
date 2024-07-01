using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SerilogAppInsightsAzureFunctions.Logging;
using SerilogAppInsightsAzureFunctions.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton<EmployeeService>();
    })
    .UseSerilog((context, services, loggerConfiguration) =>
    {
        loggerConfiguration
            .Enrich.FromLogContext()
            .Enrich.WithOperationId()
            .WriteTo.ApplicationInsights
            (
                services.GetRequiredService<TelemetryConfiguration>(),
                new OperationTelemetryConverter()
            );
    })
    .Build();

host.Run();