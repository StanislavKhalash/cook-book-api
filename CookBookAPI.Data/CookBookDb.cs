using System.Data.Entity;

namespace CookBookAPI.Data
{
    public class CookBookDb : DbContext
    {
        static CookBookDb()
        {
            System.Data.Entity.Database.SetInitializer(new CookBookDbInitializer());
        }

        public CookBookDb() : base("name=DefaultConnection")
        {
        }

        public DbSet<FoodDto> Foods { get; set; }

        public DbSet<RecipeDto> Recipes { get; set; }
    }
}
