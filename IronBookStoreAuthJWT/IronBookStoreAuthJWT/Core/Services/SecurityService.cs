using IronBookStoreAuthJWT.Core.Entities;
using IronBookStoreAuthJWT.Core.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IronBookStoreAuthJWT.Core.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IBookStoreRepository _repository;
        private readonly JwtSettingsConfiguration _jwtSettings;

        public SecurityService(IBookStoreRepository repository,
            JwtSettingsConfiguration jwtSettings)
        {
            _repository = repository;
            _jwtSettings = jwtSettings;
        }

        public async Task<string> GetToken(string email)
        {
            var user = await _repository.GetUserByEmail(email,true);
            return GenerateToken(user);
        }

        public async Task<bool> ValidateCredentials(string email, string password)
        {
            var user = await _repository.GetUserByEmail(email);

            if (user == null)
                return false;

            if (user.Password != HashHelper.Create(password))
                return false;

            return true;
        }

        private string GenerateToken(User user)
        {
            //Create token
            //Claims are knowable properties and publics - These propery will be sent to the Client.
            //We will read this Claims to get user information.

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            //Add roles
            foreach (var role in user.UserRoles.Select(r => r.Role.Name))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            //retrieve private key from appSettings, we use it to sign the token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            /*
             * Generating token with jwt settings information - this information will be sent to the client and then
             * this server will validate the token: verifying the sign | issuer | audience, so on.
             * All this fields will be checked in the [Authorize] filter | middleware.
             */

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.MinutesToExpiration),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
