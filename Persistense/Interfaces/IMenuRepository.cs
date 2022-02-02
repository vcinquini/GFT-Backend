using Domain;
using System.Threading.Tasks;

namespace Persistense.Interfaces
{
    public interface IMenuRepository
    {
        Task<Menu> AddAsync(Menu menu);
        Task<Menu> GetAsync(int dishId, int dayTimeId);
    }
}