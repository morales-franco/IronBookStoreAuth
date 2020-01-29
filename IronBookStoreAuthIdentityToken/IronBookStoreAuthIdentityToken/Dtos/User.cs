using System;

namespace IronBookStoreAuthIdentityToken.Dtos
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
