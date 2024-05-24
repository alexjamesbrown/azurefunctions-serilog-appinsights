using Serilog;
using Serilog.Configuration;

namespace SerilogAppInsightsAzureFunctions.Logging;

public static class LoggingExtensions
{
    public static LoggerConfiguration WithOperationId(this LoggerEnrichmentConfiguration enrichConfiguration)
    {
        if (enrichConfiguration is null) throw new ArgumentNullException(nameof(enrichConfiguration));

        return enrichConfiguration.With<OperationIdEnricher>();
    }
}