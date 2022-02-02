using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IOrderService
	{
		Task<string> ProcessOrderAsync(string input);
	}
}