using System;
using System.Threading.Tasks;
using IronBookStoreAuthIdentityToken.Core.Entities;

namespace IronBookStoreAuthIdentityToken.Core.Services
{
    public class SecurityService : ISecurityService
    {
        public Task<string> GetToken(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(User user, params string[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateCredentials(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
