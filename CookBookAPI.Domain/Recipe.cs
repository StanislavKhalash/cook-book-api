using System;
using System.Linq;
using System.Collections.Generic;

namespace CookBookAPI.Domain
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public double GetTotalCalories()
        {
            return Ingredients.Select(ingredient => ingredient.Food).Sum(food => food.Calories);
        }

        public bool IsVegetarian()
        {
            return Ingredients.All(ingredient => ingredient.Food.IsVegetarian);
        }
    }
}
