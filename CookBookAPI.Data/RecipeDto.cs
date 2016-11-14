using System.Collections.Generic;

namespace CookBookAPI.Data
{
    public class RecipeDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public List<IngredientDto> Ingredients { get; set; }
    }
}
