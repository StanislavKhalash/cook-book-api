using System.Linq;
using System.Security.Principal;

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

        public IQueryable<Recipe> GetAllRecipes(IPrincipal user)
        {
            return _dbContext.Recipes.Select(DomainEntityFactory.Create).AsQueryable();
        }
    }
}
