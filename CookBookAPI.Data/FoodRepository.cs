using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

using CookBookAPI.Domain;

namespace CookBookAPI.Data
{
    public class FoodRepository : IFoodRepository
    {
        private readonly CookBookDb _dbContext;

        public FoodRepository(CookBookDb dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Food> GetAll()
        {
            return _dbContext.Foods.Select(DomainEntityFactory.Create).AsQueryable();
        }

        public Food FindById(int foodId)
        {
            var foodDto = _dbContext.Foods.Find(foodId);
            return foodDto != null ? DomainEntityFactory.Create(foodDto) : null;
        }

        public int Create(Food food)
        {
            var foodDto = DomainEntityFactory.Parse(food);
            _dbContext.Foods.Add(foodDto);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to create an entity", ex);
            }

            return foodDto.Id;
        }

        public void Delete(Food food)
        {
            throw new NotImplementedException();
        }
    }
}
