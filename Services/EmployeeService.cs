using Serilog;

namespace SerilogAppInsightsAzureFunctions.Services;

public class EmployeeService
{
    private ILogger _logger;

    public EmployeeService(ILogger logger)
    {
        _logger = logger.ForContext<EmployeeService>();
    }

    public string? GetEmployeeName(string? employeeId)
    {
        if (string.IsNullOrEmpty(employeeId))
        {
            _logger.Error("Employee does not exist");
            return null;
        }

        var name = "Alex";

        _logger.Information("Successfully got employee name: {Name}", name);

        return name;
    }

    public int? GetEmployeeAge(string employeeId)
    {
        if (string.IsNullOrEmpty(employeeId) || employeeId == "0")
        {
            _logger.Error("Employee does not exist");
            return null;
        }

        var age = employeeId.Length;

        // set context "Age" for other logs
        _logger = _logger
            .ForContext("Age", age);

        _logger.Information("Successfully got employee age");

        if (age > 18)
            _logger.Warning("Employee is old");

        return age;
    }
}