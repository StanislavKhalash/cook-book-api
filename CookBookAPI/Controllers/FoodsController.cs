using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

using CookBookAPI.Domain;
using System;
using System.Web.Http.Routing;

namespace CookBookAPI.Controllers
{
    public class FoodsController : ApiController
    {
        private readonly IFoodRepository _foodRepository;

        public FoodsController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public IEnumerable<Food> Get()
        {
            return _foodRepository.GetAll().ToList();
        }

        public IHttpActionResult Get(int foodId)
        {
            var food = _foodRepository.FindById(foodId);
            if(food != null)
            {
                return Ok(food);
            }
            else
            {
                return NotFound();
            }
        }

        public IHttpActionResult Post([FromBody]Food food)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                int foodId = _foodRepository.Create(food);
                var createdFoodLocation = Url.Link("Food", new { foodId = foodId });

                return Created(createdFoodLocation, food);
            }
            catch (RepositoryException)
            {
                return BadRequest();
            }
        }
    }
}
