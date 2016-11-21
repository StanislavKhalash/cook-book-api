using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CookBookAPI.Data
{
    public class CookBookDb : IdentityDbContext<DbApplicationUser>
    {
        static CookBookDb()
        {
            Database.SetInitializer(new CookBookDbInitializer());
        }

        public CookBookDb() : base("name=DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<DbFood> Foods { get; set; }

        public DbSet<DbRecipe> Recipes { get; set; }
    }
}
