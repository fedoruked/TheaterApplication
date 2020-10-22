using Microsoft.AspNetCore.Mvc;

namespace TheaterApplication.WebApi.Authorization
{
    public class AuthByTokenAttribute: TypeFilterAttribute
    {
        public AuthByTokenAttribute(string roles = "")
          : base(typeof(AuthByTokenFilter))
        {
            Arguments = new object[] { roles };
        }
    }
}
