using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using SerilogAppInsightsAzureFunctions.Logging;
using SerilogAppInsightsAzureFunctions.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(config =>
    {
        config.AddUserSecrets<Program>(optional: true, reloadOnChange: false);
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton<EmployeeService>();
    })
    .UseSerilog((context, services, loggerConfiguration) =>
    {
        loggerConfiguration
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Worker", LogEventLevel.Warning)
            .MinimumLevel.Override("Host.*", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Error)
            .MinimumLevel.Override("Function.*", LogEventLevel.Error)
            .MinimumLevel.Override("Azure.Storage.Blobs", LogEventLevel.Error)
            .MinimumLevel.Override("Azure.Core", LogEventLevel.Error)
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