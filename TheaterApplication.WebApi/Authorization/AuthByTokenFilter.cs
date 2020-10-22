using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Linq;
using TheaterApplication.Bll.Models.Enums;
using TheaterApplication.Bll.Services.Interfaces;

namespace TheaterApplication.WebApi.Authorization
{
    public class AuthByTokenFilter : IAuthorizationFilter
    {
        private const string TokenHeaderName = "Authorization";
        private const int StatusCodeForbidden = 403;

        private readonly string _roles;
        private readonly IUserService _userService;

        public AuthByTokenFilter(string roles, IUserService userService)
        {
            _roles = roles;
            _userService = userService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = GetToken(context);
            var roles = string.IsNullOrEmpty(_roles) 
                ? null : _roles.Split(",");

            var authResult = _userService.Authorization(token, roles);

            switch (authResult)
            {
                case AuthorizationResultEnum.UserNotFound:
                case AuthorizationResultEnum.TokenExpired:
                    {
                        context.Result = new UnauthorizedResult();
                        break;
                    }
                case AuthorizationResultEnum.RoleNotAllowed:
                    {
                        context.Result = new StatusCodeResult(StatusCodeForbidden);
                        break;
                    }
            }

        }

        private string GetToken(AuthorizationFilterContext context)
        {
            string result = null;

            var headers = context.HttpContext.Request.Headers;

            if (headers.ContainsKey(TokenHeaderName))
            {
                StringValues strValues;

                if (headers.TryGetValue(TokenHeaderName, out strValues))
                {
                    result = strValues.FirstOrDefault();
                    result = result?.Replace("Bearer ", "");
                }
            }

            return result;
        }
    }
}
