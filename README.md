## Getting Started
You'll need to add your `APPLICATIONINSIGHTS_CONNECTION_STRING` - either in `local.settings.json` OR in user-secrets.  
To add to user secrets, use:

`dotnet user-secrets set APPLICATIONINSIGHTS_CONNECTION_STRING "xxx"`

Replace `xxx` with the full connection string, available from: 

<img src="https://i.imgur.com/h67nY3s.png">

---

### Useful Links
Or at least links I found useful, while putting this together

- https://benfoster.io/blog/serilog-best-practices/ 
- https://www.mostafableu.com/blog/end-to-end-telemetry-using-azure-application-insights/
- https://simonholman.dev/configure-serilog-for-logging-in-azure-functions
- https://hackernoon.com/logging-in-azure-with-application-insights-and-serilog
- https://nblumhardt.com/2016/07/serilog-2-minimumlevel-override/
- https://oleh-zheleznyak.blogspot.com/2019/08/serilog-with-application-insights.html
- https://damienbod.com/2023/08/21/asp-net-core-logging-using-serilog-and-azure/


BEGIN SCOPE
https://blog.rsuter.com/logging-with-ilogger-recommendations-and-best-practices/
---

https://github.com/ekmsystems/serilog-enrichers-correlation-id

## Possible Issues
I was sometimes getting something like
> Exception: System.InvalidOperationException: Unable to load Function 'SaveEmployee'. A function with the id '3370705404' name already exists.

https://github.com/Azure/azure-functions-dotnet-worker/issues/2124

