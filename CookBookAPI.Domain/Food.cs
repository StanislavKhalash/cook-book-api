using System.ComponentModel.DataAnnotations;

namespace CookBookAPI.Domain
{
    public class Food
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Carbonates should be greater than zero")]
        public double Carbonates { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Fats should be greater than zero")]
        public double Fats { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Proteins should be greater than zero")]
        public double Proteins { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Calories should be greater than zero")]
        public double Calories { get; set; }

        public bool IsVegetarian { get; set; }
    }
}
