using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Orders : ControllerBase
    {
        private readonly IOrderService _application;

        public Orders(IOrderService application)
        {
            _application = application;
        }

        [HttpPost()]
        public async Task<IActionResult> Post(string input)
        {
			try
			{
				if (String.IsNullOrEmpty(input))
				{
                    return BadRequest("Invalid input!");
                }
                string result = await _application.ProcessOrderAsync(input);

                return Ok(result);
			}
            catch(Exception ex)
			{
                return StatusCode(500, ex.Message);
			}
        }
    }
}
