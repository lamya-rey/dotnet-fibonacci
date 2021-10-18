using System.Collections.Generic;
using System.Threading.Tasks;
using Fibonacci;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FibonacciController: ControllerBase
    {
        private readonly Compute _compute;

        public FibonacciController(Compute compute)
        {
            _compute = compute;
        }

        [HttpGet(Name = "{number}")]
        public async Task<List<long>> Get(string number)
        {
            return await _compute.ExecuteAsync(new[] {number});
        }
    }
}