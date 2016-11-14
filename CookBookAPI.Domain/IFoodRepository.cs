using System.Linq;

namespace CookBookAPI.Domain
{
    public interface IFoodRepository
    {
        IQueryable<Food> GetAll();

        Food FindById(int foodId);

        int Create(Food food);

        void Delete(Food food);
    }
}