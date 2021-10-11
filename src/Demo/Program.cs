using System;
using System.Diagnostics;
using Fibonacci;

Stopwatch stopwatch = new();
stopwatch.Start();

using var fibonacciDataContext = new FibonacciDataContext();

var tasks = await new Fibonacci.Compute(fibonacciDataContext).ExecuteAsync(args);

foreach (var task in tasks) Console.WriteLine($"Fibo result: {task}");
stopwatch.Stop();