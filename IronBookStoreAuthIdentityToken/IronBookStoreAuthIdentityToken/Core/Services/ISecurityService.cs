using IronBookStoreAuthIdentityToken.Core.Entities;
using System.Threading.Tasks;

namespace IronBookStoreAuthIdentityToken.Core.Services
{
    public interface ISecurityService
    {
        Task<bool> ValidateCredentials(string email, string password);
        Task<string> GetToken(string email);
        Task<bool> Register(User user, params string[] roles);
    }
}
