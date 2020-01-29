using Microsoft.AspNetCore.Authorization;

namespace IronBookStoreAuthIdentityToken.Authorization
{
    public class UserMustBeBookOwnerRequirement : IAuthorizationRequirement
    {
        public string ExceptionBookName { get; private set; }

        public UserMustBeBookOwnerRequirement(string exceptionBookName)
        {
            ExceptionBookName = exceptionBookName;
        }
    }
}
