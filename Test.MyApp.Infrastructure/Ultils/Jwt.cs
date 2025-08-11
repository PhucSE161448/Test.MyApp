using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Test.MyApp.Domain.Common;
using Test.MyApp.Domain.EntityModels;

namespace Test.MyApp.Infrastructure.Ultils
{
    public static class Jwt
    {
        public static string GenerateJwtToken(User account)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey secretKey =
                new(Encoding.UTF8.GetBytes(AppConfiguration.JWTSection.SecretKey));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, account.Email),
                new Claim("Id", account.Id.ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: AppConfiguration.JWTSection.ValidIssuer,
                audience: AppConfiguration.JWTSection.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(AppConfiguration.JWTSection.AccessTokenExpiryMinutes)),
                signingCredentials: credentials);
            return jwtHandler.WriteToken(token);
        }
        public static string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
