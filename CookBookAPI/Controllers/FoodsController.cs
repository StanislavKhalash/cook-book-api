using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

using CookBookAPI.Domain;

namespace CookBookAPI.Controllers
{
    public class FoodsController : ApiController
    {
        private readonly IFoodRepository _foodRepository;

        public FoodsController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<IEnumerable<Food>> GetAsync()
        {
            return await _foodRepository.GetAllAsync();
        }

        public async Task<IHttpActionResult> GetAsync(int foodId)
        {
            var food = await _foodRepository.FindByIdAsync(foodId);
            if(food != null)
            {
                return Ok(food);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IHttpActionResult> PostAsync([FromBody]Food food)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var duplicate = await _foodRepository.FindByDescriptionAsync(food.Description);
            if (duplicate != null)
            {
                return Conflict();
            }

            _foodRepository.Create(food);

            try
            {
                await _foodRepository.SaveChangedAsync();
            }
            catch (RepositoryException)
            {
                return BadRequest();
            }

            var newFood = await _foodRepository.FindByDescriptionAsync(food.Description);
            var newFoodLocation = Url.Link("Food", new { foodId = newFood.Id });

            return Created(newFoodLocation, food);
        }
    }
}
