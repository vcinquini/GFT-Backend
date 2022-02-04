using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IFoodRepository
    {
        Task<Food> GetAsync(int id);
    }
}