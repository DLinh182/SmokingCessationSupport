using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Utils
{
    public static class JwtHelper
    {
        private static IConfiguration jwtSettings;
        static JwtHelper()
        {
            // Assuming you have a way to access the configuration, e.g., through dependency injection
            // jwtSettings = configuration.GetSection("Jwt");
            // For simplicity, let's assume jwtSettings is initialized here
            jwtSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("Jwt");
        }
        public static string GenerateJwtToken(Account account)
        {
            //var jwtSettings = _configuration.GetSection("Jwt");
            var secretKey = jwtSettings["Key"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new InvalidOperationException("JWT settings are not configured properly in appsettings.json.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                //new Claim("Email", account.GoogleId ?? ""), // Thêm GoogleId nếu có
                // Thêm Role vào claims nếu có
                new Claim(ClaimTypes.Role, account.User?.Role ?? "Member") // Mặc định là "User" nếu không có Role
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddDays(7), // Thời gian hết hạn của token
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
