using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace CookBookAPI.Domain.Tests
{
    [TestFixture]
    public class RecipeFixture
    {
        [Test]
        public void IsVegetarian_AllIngredientsShouldBeVegetarian()
        {
            var sut = new Recipe();
            sut.Ingredients = new List<Ingredient>
            {
                new Ingredient
                {
                    Food = new Food
                    {
                        Description = "Apple, Raw",
                        IsVegetarian = true
                    }
                },

                new Ingredient
                {
                    Food = new Food
                    {
                        Description = "Pear, Raw",
                        IsVegetarian = true
                    }
                }
            };

            Assert.IsTrue(sut.IsVegetarian());
        }

        [Test]
        public void IsNotVegetarian_AtLeastOneIngredientsShouldBeNotVegetarian()
        {
            var sut = new Recipe();
            sut.Ingredients = new List<Ingredient>
            {
                new Ingredient
                {
                    Food = new Food
                    {
                        Description = "Beef, Fried",
                        IsVegetarian = false
                    }
                },

                new Ingredient
                {
                    Food = new Food
                    {
                        Description = "Pear, Raw",
                        IsVegetarian = true
                    }
                }
            };

            Assert.IsFalse(sut.IsVegetarian());
        }

        [Test]
        public void GetTotalCalories_ShouldReturnComponentsTotalCalories()
        {
            var sut = new Recipe();
            sut.Ingredients = new List<Ingredient>
            {
                new Ingredient
                {
                    Food = new Food
                    {
                        Description = "Beef, Fried",
                        Calories = 200
                    }
                },

                new Ingredient
                {
                    Food = new Food
                    {
                        Description = "Pear, Raw",
                        Calories = 20
                    }
                }
            };

            var expectedTotal = 220;
            var actualTotal = sut.GetTotalCalories();

            Assert.AreEqual(expectedTotal, actualTotal);
        }
    }
}
