using ECommerce.BAL.Contractors;
using ECommerce.DAL.DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.BAL.Utilities
{
    public class JwtUtil : IJwtUtil
    {
        private readonly IConfiguration _config;
        public JwtUtil(IConfiguration config) => _config = config;

        public string GenerateAccesToken(User user)
        {
            //Get JWT Security Key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            //Get Credentials using JWT Security Key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create Identity Claims such as Actor, Name, Email, Role and etc.
            var claims = new[]
            {
                new Claim(ClaimTypes.Actor, user.InternalID.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            //Create Security Token based on the JWT Issuer, JWT Audience, Claims, Expirations and Credentials
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
                                             expires: DateTime.Now.AddHours(1), signingCredentials: credentials);

            //Create User Access Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
