namespace CookBookAPI.Data
{
    public class FoodDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public double Carbonates { get; set; }

        public double Fats { get; set; }

        public double Proteins { get; set; }

        public double Calories { get; set; }

        public bool IsVegetarian { get; set; }
    }
}
