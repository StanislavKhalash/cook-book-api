namespace CookBookAPI.Data
{
    public class DbIngredient
    {
        public int Id { get; set; }
         
        public DbFood Food;

        public double Amount;
    }
}
