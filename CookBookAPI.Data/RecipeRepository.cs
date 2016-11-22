using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using CookBookAPI.Domain;

namespace CookBookAPI.Data
{
    public sealed class RecipeRepository : IRecipeRepository
    {
        private readonly CookBookDb _dbContext;

        public RecipeRepository(CookBookDb dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Recipe> GetAllRecipes(ApplicationUser user)
        {
            return _dbContext.Recipes.Select(Mapper.Map<Recipe>);
        }
    }
}
