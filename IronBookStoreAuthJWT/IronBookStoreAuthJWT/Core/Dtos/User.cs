using System;

namespace IronBookStoreAuthJWT.Core.Dtos
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
