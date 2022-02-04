using Domain;

namespace Application.Interfaces
{
	public interface IInputValidator
	{
		void CheckInputArguments(OrderDTO inputs);
	}
}