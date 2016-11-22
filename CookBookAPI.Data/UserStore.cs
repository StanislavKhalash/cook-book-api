using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using AutoMapper;
using CookBookAPI.Domain;

namespace CookBookAPI.Data
{
    public class UserStore : IUserStore<ApplicationUser>
    {
        private readonly UserStore<DbApplicationUser> _store;

        public UserStore(CookBookDb dbContext)
        {
            _store = new UserStore<DbApplicationUser>(dbContext);
        }

        public async Task CreateAsync(ApplicationUser user)
        {
            await _store.CreateAsync(ToDbUser(user));
        }

        public async Task DeleteAsync(ApplicationUser user)
        {
            await _store.DeleteAsync(ToDbUser(user));
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            var dbUser = await _store.FindByIdAsync(userId);
            return ToDomainUser(dbUser);
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var dbUser = await _store.FindByNameAsync(userName);
            return ToDomainUser(dbUser);
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            await _store.UpdateAsync(ToDbUser(user));
        }

        public void Dispose()
        {
        }

        private static DbApplicationUser ToDbUser(ApplicationUser user)
        {
            return Mapper.Map<DbApplicationUser>(user);
        }

        private static ApplicationUser ToDomainUser(DbApplicationUser user)
        {
            return Mapper.Map<ApplicationUser>(user);
        }
    }
}
