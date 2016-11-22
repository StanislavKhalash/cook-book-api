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
                cfg.CreateMap<DbApplicationUser, ApplicationUser>().ReverseMap();
                cfg.CreateMap<DbFood, Food>().ReverseMap();
                cfg.CreateMap<DbRecipe, Recipe>().ReverseMap();
            });
        }
    }
}