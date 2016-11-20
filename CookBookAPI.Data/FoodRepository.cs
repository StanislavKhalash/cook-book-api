using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Food>> GetAllAsync()
        {
            var dbFoods = await _dbContext.Foods.ToListAsync();
            return dbFoods.Select(DomainEntityFactory.Create);
        }

        public async Task<Food> FindByIdAsync(int foodId)
        {
            var dbFood = await _dbContext.Foods.FindAsync(foodId);
            return dbFood != null ? DomainEntityFactory.Create(dbFood) : null;
        }

        public async Task<Food> FindByDescriptionAsync(string description)
        {
            var dbFood = await _dbContext.Foods.SingleOrDefaultAsync(food => food.Description == description);
            return dbFood != null ? DomainEntityFactory.Create(dbFood) : null;
        }

        public void Create(Food food)
        {
            var dbFood = DomainEntityFactory.Parse(food);
            _dbContext.Foods.Add(dbFood);
        }

        public async Task DeleteAsync(int foodId)
        {
            var dbFood = await _dbContext.Foods.FindAsync(foodId);
            _dbContext.Foods.Remove(dbFood);
        }

        public async Task SaveChangedAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to save changes to the database", ex);
            }
        }
    }
}

