using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronBookStoreAuthJWT.Core.Services
{
    public interface ISecurityService
    {
        Task<bool> ValidateCredentials(string email, string password);
        Task<string> GetToken(string email);
    }
}
