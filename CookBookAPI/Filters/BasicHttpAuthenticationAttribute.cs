using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CookBookAPI.Filters
{
    public class BasicHttpAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;

            if(authHeader != null)
            {
                if (string.Compare(authHeader.Scheme, "basic", StringComparison.OrdinalIgnoreCase) == 0 && authHeader.Parameter != null)
                {
                    var rawCredentials = authHeader.Parameter;
                    var encoding = Encoding.GetEncoding("iso-8859-1");
                    var credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));

                    var split = credentials.Split(':');
                    var username = split[0];
                    var password = split[1];
                }
            }

            HandleUnauthorized(actionContext);
        }

        private void HandleUnauthorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='CookBookAPI' location='#loginUrl'");
        }
    }
}
