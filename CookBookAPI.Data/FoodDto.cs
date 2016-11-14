namespace CookBookAPI.Data
{
    public class FoodDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Carbonates { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }

        public int Calories { get; set; }

        public bool IsVegetarian { get; set; }
    }
}
