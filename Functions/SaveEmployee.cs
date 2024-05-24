using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Serilog;

namespace SerilogAppInsightsAzureFunctions.Functions;

public class SaveEmployee
{
    private ILogger _logger;

    public SaveEmployee(ILogger logger)
    {
        _logger = logger.ForContext<SaveEmployee>();
    }

    [Function("SaveEmployee")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "employee")] HttpRequestData req)
    {
        _logger.Information("Starting to save employee");

        await Task.CompletedTask;
        throw new NotImplementedException("Not made this yet");
    }
}