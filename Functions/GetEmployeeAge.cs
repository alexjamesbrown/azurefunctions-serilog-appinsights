using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Serilog;
using Serilog.Context;
using SerilogAppInsightsAzureFunctions.Services;

namespace SerilogAppInsightsAzureFunctions.Functions;

public class GetEmployeeAge
{
    private readonly EmployeeService _employeeService;
    private ILogger _logger;

    public GetEmployeeAge(EmployeeService employeeService, ILogger logger)
    {
        _employeeService = employeeService;
        _logger = logger.ForContext<GetEmployeeName>();
    }

    [Function("GetEmployeeAge")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "employee/{employeeId}/age")] HttpRequestData req, string employeeId)
    {
        // this sets EmployeeId on all subsequent loggers
        // even in injected dependencies - like employeeQueryService
        LogContext.PushProperty("EmployeeId", employeeId);

        _logger.Information("Getting employee age");

        var age = _employeeService
            .GetEmployeeAge(employeeId);

        if (age == null)
        {
            return req.CreateResponse(HttpStatusCode.NotFound);
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(age);
        return response;
    }
}