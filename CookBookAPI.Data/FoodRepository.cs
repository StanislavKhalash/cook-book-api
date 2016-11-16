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
            var foodDtos = await _dbContext.Foods.ToListAsync();
            return foodDtos.Select(DomainEntityFactory.Create);
        }

        public async Task<Food> FindByIdAsync(int foodId)
        {
            var foodDto = await _dbContext.Foods.FindAsync(foodId);
            return foodDto != null ? DomainEntityFactory.Create(foodDto) : null;
        }

        public async Task<Food> FindByDescriptionAsync(string description)
        {
            var foodDto = await _dbContext.Foods.SingleOrDefaultAsync(food => food.Description == description);
            return foodDto != null ? DomainEntityFactory.Create(foodDto) : null;
        }

        public void Create(Food food)
        {
            var foodDto = DomainEntityFactory.Parse(food);
            _dbContext.Foods.Add(foodDto);
        }

        public void Delete(Food food)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangedAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to create an entity", ex);
            }
        }
    }
}

