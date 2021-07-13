using api_rest.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api_rest.Util
{
    public class CryptoFunctions
    {
    
        public static string GerarToken(User user, String securitityKey)
        {
            var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitityKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "teste.http",
                audience: "teste.http",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            ); ;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
