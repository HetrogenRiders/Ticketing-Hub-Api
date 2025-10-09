using TicketingHub.Api.Features.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Reflection;

namespace TicketingHub.Api.Filters
{
    public class PermissionRequirementAttribute : TypeFilterAttribute
    {
        public PermissionRequirementAttribute(string permission) : base(typeof(PermissionRequirementFilter))
        {
            Arguments = new object[] { permission };
        }
    }


    public class PermissionRequirementFilter : IAuthorizationFilter
    {
        public string Permission { get; }
        public PermissionRequirementFilter(string permission)
        {
            Permission = permission;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult(); // If not authenticated, return 401
                return;
            }

            //if (context.HttpContext.User.HasClaim(c => c.Type == "ModulePermission"))
            //{
            //    var permissions = JsonConvert.DeserializeObject<List<UserRoleClaims>>(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "ModulePermission").Value);


            //    // Get the controller name from RouteData
            //    var controllerName = context.RouteData.Values["controller"]?.ToString();

            //    if (permissions.Any(x => x.Module == controllerName))
            //    {
            //        var permission = permissions.FirstOrDefault(x => x.Module == controllerName);

            //        if (permission != null && CheckIfPropertyIsTrue(permission.Permission, Permission))
            //        {
            //            return;
            //        }
            //    }
            //}
            context.Result = new ForbidResult();
        }

        //protected static bool CheckIfPropertyIsTrue(Permission model, string propertyName)
        //{
        //    // Get the property information
        //    PropertyInfo propertyInfo = model.GetType().GetProperty(propertyName);

        //    if (propertyInfo != null)
        //    {
        //        // Get the value of the property
        //        bool propertyValue = (bool)propertyInfo.GetValue(model);
        //        return propertyValue;
        //    }

        //    return false;
        //}
    }
}
