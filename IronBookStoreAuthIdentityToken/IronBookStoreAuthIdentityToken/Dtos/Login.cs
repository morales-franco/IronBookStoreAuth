using System.ComponentModel.DataAnnotations;

namespace IronBookStoreAuthIdentityToken.Dtos
{
    public class Login
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
