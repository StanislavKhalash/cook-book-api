using AutoMapper;

using CookBookAPI.Data;
using CookBookAPI.Domain;

namespace CookBookAPI
{
    internal static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DbApplicationUser, ApplicationUser>();
                cfg.CreateMap<ApplicationUser, DbApplicationUser>();

                cfg.CreateMap<DbFood, Food>().ReverseMap();
                cfg.CreateMap<DbRecipe, Recipe>().ReverseMap();
            });
        }
    }
}