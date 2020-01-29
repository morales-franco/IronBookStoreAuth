using Microsoft.AspNetCore.Identity;
using System;

namespace IronBookStoreAuthIdentityToken.Core.Entities
{
    public class User: IdentityUser
    {
        //Identity Custom Property
        public string Alias { get; set; }
    }
}
