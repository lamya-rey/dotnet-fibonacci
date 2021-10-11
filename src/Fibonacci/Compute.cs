using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Fibonacci
{
    public class Compute
    {
        private readonly FibonacciDataContext _fibonacciDataContext;

        public Compute(FibonacciDataContext fibonacciDataContext)
        {
            _fibonacciDataContext = fibonacciDataContext;
        }

        public async Task<List<long>> ExecuteAsync(string[] arguments)
        {
            var results = await RunFibonaccisAsync(_fibonacciDataContext, arguments);
            await _fibonacciDataContext.SaveChangesAsync();
            return results;
        }
        
        private static long Fib(int i) =>
            i switch
            {
                int when i <= 2 => 1,
                _ =>  Fib(i - 2) + Fib(i - 1)
            };
        
        private static async Task<List<long>> RunFibonaccisAsync(FibonacciDataContext dataContext ,string[] strings)
        {
            var list = new List<long>();
            foreach (var input in strings)
            {
                var inputInt = Convert.ToInt32(input);
                var fibonacciFromDatabase =
                    await dataContext
                        .TFibonaccis
                        .Where(tf => tf.FibInput == inputInt)
                        .FirstOrDefaultAsync();

                if (fibonacciFromDatabase != null)
                {
                    list.Add(fibonacciFromDatabase.FibOutput);
                }
                else
                {
                    var fibOutput = await Task.Run(() => Fib(inputInt));

                    dataContext.TFibonaccis.Add(new TFibonacci()
                    {
                        FibInput = inputInt,
                        FibOutput = fibOutput,
                        FibCreatedTimestamp = DateTime.Now
                    });
                    list.Add(fibOutput);
                }
                
            }

            return list;
        }
    }
}