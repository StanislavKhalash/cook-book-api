using System.Data.Entity;

namespace CookBookAPI.Data
{
    internal sealed class CookBookDbInitializer : DropCreateDatabaseAlways<CookBookDb>
    {
        protected override void Seed(CookBookDb context)
        {
            context.Foods.Add(new FoodDto { Description = "Paprica, Raw" });
            context.Foods.Add(new FoodDto { Description = "Beef, Fried" });
            context.Foods.Add(new FoodDto { Description = "Rice, Basmati" });

            base.Seed(context);
        }
    }
}
