using IronBookStoreAuthJWT.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace IronBookStoreAuthJWT.Authorization
{
    public class UserMustBeBookOwnerRequirementHandler : AuthorizationHandler<UserMustBeBookOwnerRequirement>
    {
        private readonly IBookStoreRepository _repository;
        private readonly IHttpContextAccessor _httpContext;

        public UserMustBeBookOwnerRequirementHandler(IBookStoreRepository repository,
            IHttpContextAccessor httpContext)
        {
            _repository = repository;
            _httpContext = httpContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            UserMustBeBookOwnerRequirement requirement)
        {
            //Check exists an auth user
            if(context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier) == null)
            {
                context.Fail(); //Optional: Default: fail
                return Task.CompletedTask; //NET Framework older than 4.6: use: return Task.FromResult(0);
            }

            var userId = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            if (_httpContext.HttpContext == null)
            {
                return Task.CompletedTask; 
            }

            var bookId = _httpContext.HttpContext.Request.RouteValues["bookId"].ToString();

            if (!Guid.TryParse(bookId, out Guid bookIdAsGuid))
            {
                return Task.CompletedTask;
            }

            var book = _repository.GetBook(bookIdAsGuid).Result;

            if(book == null)
            {
                //book doesn't exist ==> responsibility of Action Method.
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if(book.Name == requirement.ExceptionBookName)
            {
                return Task.CompletedTask;
            }

            if (!Guid.TryParse(userId, out Guid userIdAsGuid))
            {
                return Task.CompletedTask;
            }

            if (!_repository.IsBookOwner(bookIdAsGuid, userIdAsGuid).Result)
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
