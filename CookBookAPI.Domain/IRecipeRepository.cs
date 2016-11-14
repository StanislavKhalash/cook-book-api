using System.Linq;

using System.Collections.Generic;
using System.Security.Principal;

namespace CookBookAPI.Domain
{
    public interface IRecipeRepository
    {
        IQueryable<Recipe> GetAllRecipes(IPrincipal user);
    }
}
