using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;

using CookBookAPI.Domain;

namespace CookBookAPI.Controllers
{
    public class RecipesController : ApiController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IRecipeRepository _recipeRepository;

        public RecipesController(
            ApplicationUserManager userManager,
            IRecipeRepository recipeRepository)
        {
            _userManager = userManager;
            _recipeRepository = recipeRepository;
        }

        public async Task<IEnumerable<Recipe>> GetAsync()
        {
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            return _recipeRepository.GetAllRecipes(user).ToList();
        }
    }
}
