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
using Ploeh.AutoFixture;

namespace CookBookAPI.Tests.Controllers
{
    [TestFixture]
    public class FoodsControllerFixture
    {
        [Test]
        public async Task Get_ShouldReturnAllFoundFoods()
        {
            Fixture fixture = new Fixture();
            var foods = fixture.CreateMany<Food>();

            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.GetAllAsync()).ReturnsAsync(foods);

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);

            var result = await controller.GetAsync();

            CollectionAssert.AreEqual(foods, result);
        }

        [Test]
        public async Task GetById_FoodDoesntExist_ShouldReturnNotFound()
        {
            Fixture fixture = new Fixture();

            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.FindByIdAsync(It.IsAny<int>())).ReturnsAsync((Food)null);

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);

            var typedResult = await controller.GetAsync(fixture.Create<int>()) as NotFoundResult;

            Assert.IsNotNull(typedResult);
        }

        [Test]
        public async Task GetById_FoodExists_ShouldReturnFoundFood()
        {
            Fixture fixture = new Fixture();

            var foodId = fixture.Create<int>();
            var food = fixture.Build<Food>().With(f => f.Id, foodId).Create();

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
            Fixture fixture = new Fixture();

            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.FindByDescriptionAsync(It.IsAny<string>())).ReturnsAsync(fixture.Create<Food>());

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);

            var typedResult = await controller.PostAsync(fixture.Create<Food>()) as ConflictResult;

            Assert.IsNotNull(typedResult);
        }

        [Test]
        public async Task Post_CreationFails_ShouldReturnBadRequest()
        {
            Fixture fixture = new Fixture();

            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.Setup(obj => obj.FindByDescriptionAsync(It.IsAny<string>())).ReturnsAsync((Food)null);
            foodRepositoryStub.Setup(obj => obj.Create(It.IsAny<Food>()));
            foodRepositoryStub.Setup(obj => obj.SaveChangedAsync()).Throws<RepositoryException>();

            FoodsController controller = new FoodsController(foodRepositoryStub.Object);

            var typedResult = await controller.PostAsync(fixture.Create<Food>()) as BadRequestResult;

            Assert.IsNotNull(typedResult);
        }

        [Test]
        public async Task Post_CreationSucceeds_ShouldReturnCreatedFood()
        {
            Fixture fixture = new Fixture();

            var food = fixture.Create<Food>();

            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.SetupSequence(obj => obj.FindByDescriptionAsync(It.IsAny<string>()))
                .ReturnsAsync(null)
                .ReturnsAsync(food);
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
            Fixture fixture = new Fixture();

            var food = fixture.Create<Food>();

            var foodRepositoryStub = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodRepositoryStub.SetupSequence(obj => obj.FindByDescriptionAsync(It.IsAny<string>()))
                .ReturnsAsync(null)
                .ReturnsAsync(food);
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
