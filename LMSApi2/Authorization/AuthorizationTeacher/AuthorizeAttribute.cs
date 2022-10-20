using LMSApi2.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using LMSApi2.Authorization.AuthorizationAnonymous;
using Microsoft.AspNetCore.Mvc;

namespace LMSApi2.Authorization.AuthorizationTeacher
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
            Console.WriteLine("in auth of teacher ");
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

           /* var user = (User)context.HttpContext.Items["User"];
            *//* || (_roles.Any() && !_roles.Contains(user.Role)*//*
            if (user == null)
            {

                context.Result = new JsonResult(new { message = "Unauthroized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }*/
        }
    }
}
