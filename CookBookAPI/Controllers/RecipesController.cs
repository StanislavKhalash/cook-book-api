using CookBookAPI.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CookBookAPI.Controllers
{
    public class RecipesController : ApiController
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIdentityService _identityService;

        public RecipesController(
            IIdentityService identityService,
            IRecipeRepository recipeRepository)
        {
            _identityService = identityService;
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Get()
        {
            return _recipeRepository.GetAllRecipes(_identityService.GetCurrentUser()).ToList();
        }
    }
}
