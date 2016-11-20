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

        public DbSet<DbFood> Foods { get; set; }

        public DbSet<DbRecipe> Recipes { get; set; }
    }
}
