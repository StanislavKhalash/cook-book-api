using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookBookAPI.Domain
{
    public interface IFoodRepository
    {
        Task<IEnumerable<Food>> GetAllAsync();

        Task<Food> FindByIdAsync(int foodId);

        Task<Food> FindByDescriptionAsync(string description);

        void Create(Food food);

        void Delete(Food food);

        Task SaveChangedAsync();
    }
}