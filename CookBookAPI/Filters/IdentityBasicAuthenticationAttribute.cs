using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

using CookBookAPI.Domain;

namespace CookBookAPI.Filters
{
    public class IdentityBasicAuthenticationAttribute : BasicAuthenticationAttribute
    {
        ApplicationUserManager _userManager;

        public IdentityBasicAuthenticationAttribute(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        protected override async Task<IPrincipal> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested(); // Unfortunately, UserManager doesn't support CancellationTokens.
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            if (user == null)
            {
                // No user with userName/password exists.
                return null;
            }

            // Create a ClaimsIdentity with all the claims for this user.
            cancellationToken.ThrowIfCancellationRequested(); // Unfortunately, IClaimsIdenityFactory doesn't support CancellationTokens.
            ClaimsIdentity identity = await _userManager.ClaimsIdentityFactory.CreateAsync(_userManager, user, "Basic");
            return new ClaimsPrincipal(identity);
        }
    }
}