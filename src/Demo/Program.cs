using System;
using System.Diagnostics;

Stopwatch stopwatch = new();
stopwatch.Start();
var tasks = await Fibonacci.Compute.ExecuteAsync(args);

foreach (var task in tasks) Console.WriteLine($"Fibo result: {task}");
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed.Seconds} s");