using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronBookStoreAuthIdentityToken.Core.Services
{
    public interface IUserInfoService
    {
        Guid UserId { get; set; }
        string Email { get; set; }
        List<string> Roles { get; set; }
    }
}
