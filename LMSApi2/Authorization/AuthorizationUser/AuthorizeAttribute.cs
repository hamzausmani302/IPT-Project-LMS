using LMSApi2.Models;
using Microsoft.AspNetCore.Mvc;
using LMSApi2.Authorization.AuthorizationAnonymous;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LMSApi2.Authorization.AuthorizationUser
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Console.WriteLine("in auth of user ");
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var user = (User)context.HttpContext.Items["User"];
            if (user == null || (_roles.Any() && !_roles.Contains(user.Role)))
            {

                context.Result = new JsonResult(new { message = "Unauthroized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
