using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using Ploeh.AutoFixture;

namespace CookBookAPI.Domain.Tests
{
    [TestFixture]
    public class RecipeFixture
    {
        [Test]
        public void IsVegetarian_AllIngredientsShouldBeVegetarian()
        {
            Fixture fixture = new Fixture();
            fixture.Customize(VeggieFoodCustomization());

            var sut = new Recipe();
            sut.Ingredients = fixture.CreateMany<Ingredient>().ToList();

            Assert.IsTrue(sut.IsVegetarian());
        }

        [Test]
        public void IsNotVegetarian_AtLeastOneIngredientsShouldBeNotVegetarian()
        {
            Fixture fixture = new Fixture();
            fixture.Customize(VeggieFoodCustomization());

            var sut = new Recipe();
            sut.Ingredients = fixture.CreateMany<Ingredient>().ToList();

            fixture.Customize(NonVeggieFoodCustomization());
            sut.Ingredients.Add(fixture.Create<Ingredient>());

            Assert.IsFalse(sut.IsVegetarian());
        }

        [Test]
        public void GetTotalCalories_ShouldReturnComponentsTotalCalories()
        {
            Fixture fixture = new Fixture();

            var sut = new Recipe();
            sut.Ingredients = fixture.CreateMany<Ingredient>().ToList();

            var expectedTotal = sut.Ingredients.Sum(i => i.Food.Calories);
            var actualTotal = sut.GetTotalCalories();

            Assert.AreEqual(expectedTotal, actualTotal);
        }

        private class FoodCustomization : ICustomization
        {
            private readonly bool _isVeggie;

            public FoodCustomization(bool isVeggie)
            {
                _isVeggie = isVeggie;
            }

            public void Customize(IFixture fixture)
            {
                fixture.Customize<Food>(c => c.With(f => f.IsVegetarian, _isVeggie));
            }
        }

        private static FoodCustomization VeggieFoodCustomization()
        {
            return new FoodCustomization(true);
        }

        private static FoodCustomization NonVeggieFoodCustomization()
        {
            return new FoodCustomization(false);
        }
    }
}
