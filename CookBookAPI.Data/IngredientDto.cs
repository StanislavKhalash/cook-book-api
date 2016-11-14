namespace CookBookAPI.Data
{
    public class IngredientDto
    {
        public int Id { get; set; }
         
        public FoodDto Food;

        public double Amount;
    }
}
