using API;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Controller.Tests
{
	public class OrdersControllerTests : IClassFixture<WebAppFactory<Startup>>
	{
		private readonly HttpClient _client;

		public OrdersControllerTests(WebAppFactory<Startup> factory)
		{
			_client = factory.CreateClient();
		}

		[Theory]
		[InlineData("morning, 1, 2, 3", "Output: Eggs, Toast, Coffe")]
		[InlineData("morning, 1, 2, 3, 3, 3", "Output: Eggs, Toast, Coffe(x3)")]
		[InlineData("morning, 1, 2, 3, 4", "Output: Eggs, Toast, Coffe, Error")]
		[InlineData("morning, 1, 2, 8, 4", "Output: Eggs, Toast, Error")]
		[InlineData("morning, 1, 2, 2, 4", "Output: Eggs, Toast, Error")]
		[InlineData("night, 1, 2, 3, 4", "Output: Steak, Potato, Wine, Cake")]
		[InlineData("night, 1, 2, 2, 4", "Output: Steak, Potato(x2), Cake")]
		[InlineData("night, 1, 3, 4", "Output: Steak, Wine, Cake")]
		[InlineData("night, 1, 3, 5", "Output: Steak, Wine, Error")]
		[InlineData("1, 2, 3", "Invalid time of day!")]
		[InlineData("", "Invalid input!")]
		[InlineData("morning", "Invalid input argument! You must select at least one dish type.")]
		public async Task Post_Order(string input, string output)
		{
			var parameters = String.Join('=', new string[] { "input", input });
			
			var message = new HttpRequestMessage(HttpMethod.Post, $"/Orders?{parameters}");
			
			var httpResponse = await _client.SendAsync(message);

			var response = await httpResponse.Content.ReadAsStringAsync();

			response
				.Should()
				.Be(output);
		}
	}
}
