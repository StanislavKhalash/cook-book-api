using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using AutoMapper;
using CookBookAPI.Domain;
using System;

namespace CookBookAPI.Data
{
    public class ApplicationUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>
    {
        private readonly UserStore<DbApplicationUser> _store;

        public ApplicationUserStore(CookBookDb dbContext)
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

        public async Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            await _store.SetPasswordHashAsync(ToDbUser(user), passwordHash);
        }

        public async Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return await _store.GetPasswordHashAsync(ToDbUser(user));
        }

        public async Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return await _store.HasPasswordAsync(ToDbUser(user));
        }

        public async Task SetEmailAsync(ApplicationUser user, string email)
        {
            await _store.SetEmailAsync(ToDbUser(user), email);
        }

        public async Task<string> GetEmailAsync(ApplicationUser user)
        {
            return await _store.GetEmailAsync(ToDbUser(user));
        }

        public async Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
        {
            return await _store.GetEmailConfirmedAsync(ToDbUser(user));
        }

        public async Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            await _store.SetEmailConfirmedAsync(ToDbUser(user), confirmed);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var dbUser = await _store.FindByEmailAsync(email);
            return ToDomainUser(dbUser);
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
