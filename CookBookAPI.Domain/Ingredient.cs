namespace CookBookAPI.Domain
{
    public class Ingredient
    {
        public int Id { get; set; }

        public Food Food;

        public double Amount;
    }
}
