using Domain.DTOs;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IOrderService
	{
		Task<string> ProcessOrderAsync(OrderDTO order);
	}
}