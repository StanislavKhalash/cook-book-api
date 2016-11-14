using System.Linq;

using CookBookAPI.Domain;

namespace CookBookAPI.Data
{
    public static class DomainEntityFactory
    {
        public static Food Create(FoodDto foodDto)
        {
            return new Food
            {
                Id = foodDto.Id,
                Description = foodDto.Description,
                Carbonates = foodDto.Carbonates,
                Fats = foodDto.Fats,
                Proteins = foodDto.Proteins,
                Calories = foodDto.Calories,
                IsVegetarian = foodDto.IsVegetarian
            };
        }

        public static Ingredient Create(IngredientDto ingredientDto)
        {
            return new Ingredient
            {
                Id = ingredientDto.Id,
                Food = Create(ingredientDto.Food),
                Amount = ingredientDto.Amount
            };
        }

        public static Recipe Create(RecipeDto recipeDto)
        {
            return new Recipe
            {
                Id = recipeDto.Id,
                Description = recipeDto.Description,
                Ingredients = recipeDto.Ingredients.Select(Create).ToList()
            };
        }

        public static FoodDto Parse(Food food)
        {
            return new FoodDto
            {
                Description = food.Description,
                Carbonates = food.Carbonates,
                Fats = food.Fats,
                Proteins = food.Proteins,
                Calories = food.Calories,
                IsVegetarian = food.IsVegetarian
            };
        }
    }
}
