using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IronBookStoreAuthJWT.Core.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Guid UserId { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        public UserInfoService(IHttpContextAccessor httpContextAccessor)
        {
            // service is scoped, created once for each request => we only need
            // to fetch the info in the constructor
            _httpContextAccessor = httpContextAccessor
                ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            var currentContext = _httpContextAccessor.HttpContext;
            if (currentContext == null || currentContext.User == null || 
                currentContext.User.Claims == null || 
                !currentContext.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                return;
            }

            Roles = new List<string>();

            var userId = currentContext
               .User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if(!Guid.TryParse(userId, out Guid userIdAsGuid))
            {
                userIdAsGuid = Guid.Empty;
            }

            UserId = userIdAsGuid;

            var roles = currentContext.User.Claims.Where(c => c.Type == ClaimTypes.Role);

            foreach (var role in roles)
            {
                Roles.Add(role.Value);
            }

            Email = currentContext.User
               .Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }
    }
}
