using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;

using Moq;
using CookBookAPI.Controllers;
using CookBookAPI.Domain;
using NUnit.Framework;

namespace CookBookAPI.Tests.Controllers
{
    [TestFixture]
    public class FoodsControllerFixture
    {
        [Test]
        public async Task Get_ShouldReturnAllFoundFoods()
        {
            var foods = new List<Food>
            {
                new Food { Id = 1, Description = "Beef, Fried" },
                new Food { Id = 2, Description = "Pear, Raw" }
            };

            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.GetAllAsync()).ReturnsAsync(foods);

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);

            var result = await controller.GetAsync();

            CollectionAssert.AreEqual(foods, result.ToList());
        }

        [Test]
        public async Task GetById_FoodDoesntExist_ShouldReturnNotFound()
        {
            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.FindByIdAsync(It.IsAny<int>())).ReturnsAsync((Food)null);

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);

            int foodId = 1;
            var typedResult = await controller.GetAsync(foodId) as NotFoundResult;

            Assert.IsNotNull(typedResult);
        }

        [Test]
        public async Task GetById_FoodExists_ShouldReturnFoundFood()
        {
            var foodId = 1;
            var food = new Food { Id = foodId, Description = "Beef, Fried" };

            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.FindByIdAsync(foodId)).ReturnsAsync(food);
            foodRepositoryStub.Setup(obj => obj.SaveChangedAsync());
            FoodsController controller = new FoodsController(foodRepositoryStub.Object);

            var typedResult = await controller.GetAsync(foodId) as OkNegotiatedContentResult<Food>;

            Assert.IsNotNull(typedResult);
            Assert.AreSame(food, typedResult.Content);
        }

        [Test]
        public async Task Post_CreatingDuplicate_ShouldReturnConflict()
        {
            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.FindByDescriptionAsync(It.IsAny<string>())).ReturnsAsync(new Food());

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);

            var typedResult = await controller.PostAsync(new Food()) as ConflictResult;

            Assert.IsNotNull(typedResult);
        }

        [Test]
        public async Task Post_CreationFails_ShouldReturnBadRequest()
        { 
            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.FindByDescriptionAsync(It.IsAny<string>())).ReturnsAsync((Food)null);
            foodRepositoryStub.Setup(obj => obj.Create(It.IsAny<Food>()));
            foodRepositoryStub.Setup(obj => obj.SaveChangedAsync()).Throws<RepositoryException>();

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);

            var typedResult = await controller.PostAsync(new Food()) as BadRequestResult;

            Assert.IsNotNull(typedResult);
        }

        [Test]
        public async Task Post_CreationSucceeds_ShouldReturnCreatedFood()
        {
            var food = new Food { Description = "Pork, Fried", Calories = 250 };

            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.SetupSequence(obj => obj.FindByDescriptionAsync(It.IsAny<string>()))
                .ReturnsAsync(null)
                .ReturnsAsync(new Food { Id = 1, Description = "Pork, Fried", Calories = 250 });
            foodRepositoryStub.Setup(obj => obj.Create(food));
            foodRepositoryStub.Setup(obj => obj.SaveChangedAsync()).Returns(Task.CompletedTask);

            var urlHelperStub = new Mock<UrlHelper>();
            urlHelperStub.Setup(obj => obj.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(string.Empty);

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);
            controller.Url = urlHelperStub.Object;

            var typedResult = await controller.PostAsync(food) as CreatedNegotiatedContentResult<Food>;
            
            Assert.IsNotNull(typedResult);

            Assert.AreSame(food, typedResult.Content);
        }

        [Test]
        public async Task Post_CreationSucceeds_ShouldReturnCreatedFoodLocation()
        {
            var food = new Food { Description = "Pork, Fried", Calories = 250 };

            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.SetupSequence(obj => obj.FindByDescriptionAsync(It.IsAny<string>()))
                .ReturnsAsync(null)
                .ReturnsAsync(new Food { Id = 1, Description = "Pork, Fried", Calories = 250 });
            foodRepositoryStub.Setup(obj => obj.Create(food));
            foodRepositoryStub.Setup(obj => obj.SaveChangedAsync()).Returns(Task.CompletedTask);

            var locationUrl = "http://location/";
            var urlHelperStub = new Mock<UrlHelper>(MockBehavior.Strict);
            urlHelperStub.Setup(obj => obj.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(locationUrl);

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Url = urlHelperStub.Object;

            var typedResult = await controller.PostAsync(food) as CreatedNegotiatedContentResult<Food>;

            Assert.AreSame(locationUrl, typedResult.Location.AbsoluteUri);
        }
    }
}
