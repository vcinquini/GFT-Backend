using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IMenuRepository
    {
        Task<Menu> GetAsync(int dishId, int dayTimeId);
    }
}