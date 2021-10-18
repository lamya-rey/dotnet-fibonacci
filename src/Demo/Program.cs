using System;
using System.Diagnostics;
using System.IO;
using Fibonacci;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");             
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
    .Build();       
var applicationSection = configuration.GetSection("Application");
var applicationConfig = applicationSection.Get<ApplicationConfig>();
Console.WriteLine($"Application Name : {applicationConfig.Name}");
Console.WriteLine($"Application Message : {applicationConfig.Message}");

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddFilter("Microsoft", LogLevel.Warning)                 
        .AddFilter("System", LogLevel.Warning)                 
        .AddFilter("Demo", LogLevel.Debug)                 
        .AddConsole();
}     );     
var logger = loggerFactory.CreateLogger("Demo.Program");          
logger.LogInformation($"Application Name : {applicationConfig.Name}");    
logger.LogInformation($"Application Message : {applicationConfig.Message}");



Stopwatch stopwatch = new();
stopwatch.Start();

using var fibonacciDataContext = new FibonacciDataContext();

var tasks = await new Fibonacci.Compute(fibonacciDataContext).ExecuteAsync(args);

foreach (var task in tasks) Console.WriteLine($"Fibo result: {task}");
stopwatch.Stop();

public class ApplicationConfig
{
    public string Name { get; set; }
    public string Message { get; set; }
}