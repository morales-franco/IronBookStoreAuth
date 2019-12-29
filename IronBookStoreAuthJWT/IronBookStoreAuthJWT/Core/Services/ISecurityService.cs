using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IronBookStoreAuthJWT.Core.Entities;

namespace IronBookStoreAuthJWT.Core.Services
{
    public interface ISecurityService
    {
        Task<bool> ValidateCredentials(string email, string password);
        Task<string> GetToken(string email);
        Task<bool> Register(User user, params string[] roles);
    }
}
