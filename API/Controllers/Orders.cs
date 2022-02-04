using Application.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [EnableCors("corsPolicy")]
        public async Task<IActionResult> Post(OrderDTO order)
        {
			try
			{
                string result = await _application.ProcessOrderAsync(order);

                return Ok(result);
			}
            catch(ArgumentException ex)
			{
                return BadRequest(ex.Message);
			}
            catch(Exception ex)
			{
                return StatusCode(500, ex.Message);
			}
        }
    }
}
