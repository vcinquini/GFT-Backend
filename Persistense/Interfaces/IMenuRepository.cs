using Domain;
using System.Threading.Tasks;

namespace Persistense.Interfaces
{
    public interface IMenuRepository
    {
        Task<Menu> AddAsync(Menu menu);
        //Task DeleteAsync(Menu menu);
        Task<Menu> GetAsync(int dishId, int dayTimeId);
        //Task UpdateAsync(Menu menu);
    }
}