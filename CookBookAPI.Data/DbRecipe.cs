using System.Collections.Generic;

namespace CookBookAPI.Data
{
    public class DbRecipe
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public List<DbIngredient> Ingredients { get; set; }
    }
}
