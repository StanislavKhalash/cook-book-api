using System.Collections.Generic;

namespace CookBookAPI.Domain
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> GetAllRecipes(ApplicationUser user);
    }
}
