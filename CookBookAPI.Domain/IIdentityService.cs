using System.Security.Principal;

namespace CookBookAPI.Domain
{
    public interface IIdentityService
    {
        IPrincipal GetCurrentUser();
    }
}
