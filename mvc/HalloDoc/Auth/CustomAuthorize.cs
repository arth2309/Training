using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using HallodocServices.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HalloDoc.Auth
{
    [AttributeUsage(AttributeTargets.All)]
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        private readonly string _role;

        public CustomAuthorize(string role = "")
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext) 
        {

            if (_role == "Patient")
            {
                var jwtService = filterContext.HttpContext.RequestServices.GetService<IJwtServices>();
                if (jwtService == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Patient", action = "PatientLogin" }));
                    return;
                }

                var request = filterContext.HttpContext.Request;
                var token = request.Cookies["token"];
                if (token == null || !jwtService.ValidateToken(token, out JwtSecurityToken jwtSecurityToken))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Patient", action = "PatientLogin" }));
                    return;
                }

                var roleclaim = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
                if (roleclaim == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Patient", action = "PatientLogin" }));
                    return;
                }

                if (String.IsNullOrWhiteSpace(_role) || roleclaim.Value != _role)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Patient", action = "AccessDenied" }));
                }

            }
            else
            {
                var jwtService = filterContext.HttpContext.RequestServices.GetService<IJwtServices>();
                if (jwtService == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AdminSite", action = "AdminLogin" }));
                    return;
                }

                var request = filterContext.HttpContext.Request;
                var token = request.Cookies["token"];
                if (token == null || !jwtService.ValidateToken(token, out JwtSecurityToken jwtSecurityToken))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AdminSite", action = "AdminLogin" }));
                    return;
                }

                var roleclaim = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
                if (roleclaim == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AdminSite", action = "AdminLogin" }));
                    return;
                }

                if (String.IsNullOrWhiteSpace(_role) || roleclaim.Value != _role)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Patient", action = "AccessDenied" }));
                }
            }
        }


    }
}
