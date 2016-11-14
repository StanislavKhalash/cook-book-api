using System.Security.Principal;
using System.Threading;

namespace CookBookAPI.Domain
{
    public class IdentityService : IIdentityService
    {
        public IPrincipal GetCurrentUser()
        {
            return Thread.CurrentPrincipal;
        }
    }
}
