using System.Linq;

using CookBookAPI.Domain;

namespace CookBookAPI.Data
{
    public static class DomainEntityFactory
    {
        public static Food Create(DbFood dbFood)
        {
            return new Food
            {
                Id = dbFood.Id,
                Description = dbFood.Description,
                Carbonates = dbFood.Carbonates,
                Fats = dbFood.Fats,
                Proteins = dbFood.Proteins,
                Calories = dbFood.Calories,
                IsVegetarian = dbFood.IsVegetarian
            };
        }

        public static Ingredient Create(DbIngredient dbIngredient)
        {
            return new Ingredient
            {
                Id = dbIngredient.Id,
                Food = Create(dbIngredient.Food),
                Amount = dbIngredient.Amount
            };
        }

        public static Recipe Create(DbRecipe dbRecipe)
        {
            return new Recipe
            {
                Id = dbRecipe.Id,
                Description = dbRecipe.Description,
                Ingredients = dbRecipe.Ingredients.Select(Create).ToList()
            };
        }

        public static DbFood Parse(Food dbFood)
        {
            return new DbFood
            {
                Description = dbFood.Description,
                Carbonates = dbFood.Carbonates,
                Fats = dbFood.Fats,
                Proteins = dbFood.Proteins,
                Calories = dbFood.Calories,
                IsVegetarian = dbFood.IsVegetarian
            };
        }
    }
}
