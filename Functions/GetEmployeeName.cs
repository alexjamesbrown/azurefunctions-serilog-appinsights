using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Serilog;
using Serilog.Context;
using SerilogAppInsightsAzureFunctions.Services;

namespace SerilogAppInsightsAzureFunctions.Functions;

public class GetEmployeeName
{
    private readonly EmployeeService _employeeService;
    private readonly ILogger _logger;

    public GetEmployeeName(EmployeeService employeeService, ILogger logger)
    {
        _employeeService = employeeService;
        _logger = logger.ForContext<GetEmployeeName>();
    }

    [Function(nameof(GetEmployeeName))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "employee/{employeeId}/name")] HttpRequestData req, string employeeId)
    {
        // this sets EmployeeId on all subsequent loggers
        // even in injected dependencies - like employeeQueryService
        LogContext.PushProperty("EmployeeId", employeeId);

        _logger.Information("Getting employee name");

        var employee = _employeeService
            .GetEmployeeName(employeeId);

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(employee);
        return response;
    }
}