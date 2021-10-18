using System;
using System.Diagnostics;
using System.IO;
using Fibonacci;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
    .Build();
var applicationSection = configuration.GetSection("Application");
var applicationConfig = applicationSection.Get<ApplicationConfig>();


var services = new ServiceCollection();  
services.AddTransient<FibonacciDataContext>();  
services.AddTransient<Compute>();  
services.AddLogging(configure => configure.AddConsole());

using (var serviceProvider = services.BuildServiceProvider())
{
    var logger =serviceProvider.GetService<ILogger<Compute>>();
    logger.LogError($"Application Name : {applicationConfig.Name}");
    logger.LogInformation($"Application Message : {applicationConfig.Message}");


    Stopwatch stopwatch = new();
    stopwatch.Start();
    var compute = serviceProvider.GetService<Compute>();
    var tasks = await compute.ExecuteAsync(args);
    foreach (var task in tasks) Console.WriteLine($"Fibo result : {task}");
    stopwatch.Stop();
    Console.WriteLine($"{stopwatch.Elapsed.Seconds}s");
}

public class ApplicationConfig
{
    public string Name { get; set; }
    public string Message { get; set; }
}