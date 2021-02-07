using EcCoach.Web.Interfaces;
using EcCoach.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcCoach.Web.Extentions
{
    public class CurrentUserFactory : ICurrentUserFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public CurrentUser Get
        {
            get {
                var result = new CurrentUser();
                if(_httpContextAccessor.HttpContext == null || !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return result;
                }

                var claims = _httpContextAccessor.HttpContext.User.Claims.ToList();

                result.UserId = claims.First().Value;

                if (claims.Any(x => x.Type.Equals(ClaimTypes.Name)))
                    result.Name = claims.Where(x => x.Type.Equals(ClaimTypes.Name)).First().Value;
                if (claims.Any(x => x.Type.Equals(ClaimTypes.Email)))
                    result.Email = claims.Where(x => x.Type.Equals(ClaimTypes.Email)).First().Value;
                if (claims.Any(x => x.Type.Equals("OwnerId")))
                    result.OwnerId = int.Parse( claims.Where(x => x.Type.Equals("OwnerId")).First().Value);

                return result;
            }
        }
    }
}
