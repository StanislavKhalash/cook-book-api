using System.Data.Entity;

namespace CookBookAPI.Data
{
    internal sealed class CookBookDbInitializer : DropCreateDatabaseAlways<CookBookDb>
    {
        protected override void Seed(CookBookDb context)
        {
            context.Foods.Add(new DbFood { Description = "Paprica, Raw" });
            context.Foods.Add(new DbFood { Description = "Beef, Fried" });
            context.Foods.Add(new DbFood { Description = "Rice, Basmati" });

            base.Seed(context);
        }
    }
}
