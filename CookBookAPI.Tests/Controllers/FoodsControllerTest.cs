using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using CookBookAPI.Controllers;
using CookBookAPI.Domain;
using System.Web.Http.Routing;

namespace CookBookAPI.Tests.Controllers
{
    [TestClass]
    public class FoodsControllerTest
    {
        [TestMethod]
        public void Get_ShouldReturnAllFoundFoods()
        {
            var foods = new List<Food>
            {
                new Food { Id = 1, Description = "Beef, Fried" },
                new Food { Id = 2, Description = "Pear, Raw" }
            };

            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.GetAll()).Returns(foods.AsQueryable());

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);

            var result = controller.Get().ToList();

            CollectionAssert.AreEqual(foods, result);
        }

        [TestMethod]
        public void GetById_FoodDoesntExist_ShouldReturnNotFound()
        {
            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.FindById(It.IsAny<int>())).Returns<Food>(null);

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);

            int foodId = 1;
            var typedResult = controller.Get(foodId) as NotFoundResult;

            Assert.IsNotNull(typedResult);
        }

        [TestMethod]
        public void GetById_FoodExists_ShouldReturnFoundFood()
        {
            var foodId = 1;
            var food = new Food { Id = foodId, Description = "Beef, Fried" };

            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.FindById(foodId)).Returns(food);

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);

            var typedResult = controller.Get(foodId) as OkNegotiatedContentResult<Food>;

            Assert.IsNotNull(typedResult);
            Assert.AreSame(food, typedResult.Content);
        }

        [TestMethod]
        public void Post_CreationFails_ShouldReturnBadRequest()
        {
            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.Create(It.IsAny<Food>())).Throws<RepositoryException>();

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);

            var typedResult = controller.Post(new Food()) as BadRequestResult;

            Assert.IsNotNull(typedResult);
        }

        [TestMethod]
        public void Post_CreationSucceeds_ShouldReturnCreatedFood()
        {
            var food = new Food { Description = "Pork, Fried", Calories = 250 };

            int generatedId = 1;
            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.Create(food)).Returns(generatedId);

            var urlHelperStub = new Mock<UrlHelper>();
            urlHelperStub.Setup(obj => obj.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(string.Empty);

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);
            controller.Url = urlHelperStub.Object;

            var typedResult = controller.Post(food) as CreatedNegotiatedContentResult<Food>;
            
            Assert.IsNotNull(typedResult);

            food.Id = generatedId;
            Assert.AreSame(food, typedResult.Content);
        }

        [TestMethod]
        public void Post_CreationSucceeds_ShouldReturnCreatedFoodLocation()
        {
            var food = new Food { Description = "Pork, Fried", Calories = 250 };

            int generatedId = 1;
            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.Create(food)).Returns(generatedId);

            var locationUrl = "http://location/";
            var urlHelperStub = new Mock<UrlHelper>(MockBehavior.Strict);
            urlHelperStub.Setup(obj => obj.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(locationUrl);

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Url = urlHelperStub.Object;

            var typedResult = controller.Post(food) as CreatedNegotiatedContentResult<Food>;

            Assert.AreSame(locationUrl, typedResult.Location.AbsoluteUri);
        }
    }
}
