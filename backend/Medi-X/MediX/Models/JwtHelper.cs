using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace MediX.Models
{
    public static class JwtHelper
    {
        public static readonly byte[] secretKey = Encoding.UTF8.GetBytes("this is my custom Secret key for authentication");

        public static string CreateJwtToken(string email, string role)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
            };

            JwtSecurityTokenHandler h = new JwtSecurityTokenHandler();

            var token = h.CreateToken(new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddHours(6),
            });

            return h.WriteToken(token);
        }

        public static ClaimsPrincipal ValidateJwtToken(string token)
        {
            JwtSecurityTokenHandler h = new JwtSecurityTokenHandler();
            h.ValidateToken(token, new TokenValidationParameters()
            {
                ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero

            }, out var securityToken);

            var jwt = securityToken as JwtSecurityToken;
            var id = new ClaimsIdentity(jwt.Claims, "jwt", "name", "role");

            return new ClaimsPrincipal(id);
        }

        public static bool AuthenticationRequest(HttpRequestMessage request, out string role)
        {
            try
            {
                IEnumerable<string> values = new List<string>();
                request.Headers.TryGetValues("Authorization", out values);
                var token = values.First();

                var principal = ValidateJwtToken(token);
                role = principal.FindFirst("Role").Value;
                return true;
            }
            catch { }
            role = "Unauthorized";
            return false;
        }
    }
}